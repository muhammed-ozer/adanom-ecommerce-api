using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateLocalDeliveryProviderHandler : IRequestHandler<UpdateLocalDeliveryProvider, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateLocalDeliveryProviderHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateLocalDeliveryProvider command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var localDeliveryProvider = await applicationDbContext.LocalDeliveryProviders
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .Include(e => e.LocalDeliveryProvider_AddressDistrict_Mappings)
                .SingleAsync();

            if (command.IsDefault && !localDeliveryProvider.IsDefault)
            {
                var cuurentDefaultShippingprovider = await applicationDbContext.LocalDeliveryProviders
                    .Where(e => e.DeletedAtUtc == null &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (cuurentDefaultShippingprovider != null)
                {
                    cuurentDefaultShippingprovider.IsDefault = false;
                }
            }
            else
            {
                command.IsDefault = true;
            }

            localDeliveryProvider = _mapper.Map(command, localDeliveryProvider, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedByUserId = userId;
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            // Address distirct ids reqquesting from command
            var requestingAddressDistrictIds = command.SupportedAddressDistrictIds;

            // Existing ids
            var currentSupportedAddressDistrictIds = localDeliveryProvider.LocalDeliveryProvider_AddressDistrict_Mappings.Select(e => e.AddressDistrictId).ToList();

            // Needs to be create address distirct ids
            var addressDistrictIdsToCreate = requestingAddressDistrictIds.Except(currentSupportedAddressDistrictIds).ToList();

            // Needs to be removed address distirct ids
            var addressDistrictIdsToRemove = currentSupportedAddressDistrictIds.Except(requestingAddressDistrictIds).ToList();

            // Create new mappings
            foreach (var addressDistrictId in addressDistrictIdsToCreate)
            {
                var createMappingRequest = new CreateLocalDeliveryProvider_AddressDistrictRequest()
                {
                    LocalDeliveryProviderId = localDeliveryProvider.Id,
                    AddressDistrictId = addressDistrictId
                };

                var createLocalDeliveryProvider_AddressDistrictCommand = _mapper.Map(createMappingRequest, new CreateLocalDeliveryProvider_AddressDistrict(command.Identity));

                await _mediator.Send(createLocalDeliveryProvider_AddressDistrictCommand);
            }

            // Remove mappings
            foreach (var addressDistrictId in addressDistrictIdsToRemove)
            {
                var deleteMappingRequest = new DeleteLocalDeliveryProvider_AddressDistrictRequest()
                {
                    LocalDeliveryProviderId = command.Id,
                    AddressDistrictId = addressDistrictId
                };

                var deleteMappingCommand = _mapper.Map(deleteMappingRequest, new DeleteLocalDeliveryProvider_AddressDistrict(command.Identity));

                await _mediator.Send(deleteMappingCommand);
            }

            applicationDbContext.Update(localDeliveryProvider);
            await applicationDbContext.SaveChangesAsync();
            
            await _mediator.Publish(new ClearEntityCache<LocalDeliveryProviderResponse>());

            return true;
        }

        #endregion
    }
}
