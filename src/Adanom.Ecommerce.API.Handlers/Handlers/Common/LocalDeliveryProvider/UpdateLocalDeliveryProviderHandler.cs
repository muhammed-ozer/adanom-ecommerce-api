using System.Security.Claims;
using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateLocalDeliveryProviderHandler : IRequestHandler<UpdateLocalDeliveryProvider, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateLocalDeliveryProviderHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateLocalDeliveryProvider command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var localDeliveryProvider = await _applicationDbContext.LocalDeliveryProviders
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .Include(e => e.SupportedAddressDistricts)
                .SingleAsync();

            if (command.IsDefault && !localDeliveryProvider.IsDefault)
            {
                var cuurentDefaultShippingprovider = await _applicationDbContext.LocalDeliveryProviders
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

            // Address distirct ids incoming from command
            var incomingIds = command.SupportedAddressDistrictIds;

            // Existing ids
            var existingIds = localDeliveryProvider.SupportedAddressDistricts.Select(e => e.Id).ToList();

            // Needs to be add address distirct ids
            var idsToAdd = incomingIds.Except(existingIds).ToList();

            // Needs to be removed address distirct ids
            var idsToRemove = existingIds.Except(incomingIds).ToList();

            // Add new address districts
            foreach (var idToAdd in idsToAdd)
            {
                var addressDistrict = await _applicationDbContext.AddressDistricts
                    .Where(e => e.Id == idToAdd)
                    .SingleAsync();

                localDeliveryProvider.SupportedAddressDistricts.Add(addressDistrict);
            }

            // Remove address districts
            foreach (var idToRemove in idsToRemove)
            {
                var addressDistrictToRemove = localDeliveryProvider.SupportedAddressDistricts.FirstOrDefault(e => e.Id == idToRemove);

                if (addressDistrictToRemove != null)
                {
                    localDeliveryProvider.SupportedAddressDistricts.Remove(addressDistrictToRemove);
                }
            }

            _applicationDbContext.Update(localDeliveryProvider);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.LOCALDELIVERYPROVIDER,
                    TransactionType = TransactionType.UPDATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new ClearEntityCache<LocalDeliveryProviderResponse>());

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.LOCALDELIVERYPROVIDER,
                TransactionType = TransactionType.UPDATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, localDeliveryProvider.Id),
            }));

            return true;
        }

        #endregion
    }
}
