using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateLocalDeliveryProviderHandler : IRequestHandler<CreateLocalDeliveryProvider, LocalDeliveryProviderResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateLocalDeliveryProviderHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<LocalDeliveryProviderResponse?> Handle(CreateLocalDeliveryProvider command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (!command.IsDefault)
            {
                var hasAnyOtherLocalDeliveryProvider = await applicationDbContext.LocalDeliveryProviders
                    .AsNoTracking()
                    .AnyAsync(e => e.DeletedAtUtc == null);

                if (!hasAnyOtherLocalDeliveryProvider)
                {
                    command.IsDefault = true;
                }
            }
            else
            {
                var currentDefaultLocalDeliveryProvider = await applicationDbContext.LocalDeliveryProviders
                    .Where(e => e.DeletedAtUtc == null &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (currentDefaultLocalDeliveryProvider != null)
                {
                    currentDefaultLocalDeliveryProvider.IsDefault = false;
                }
            }

            var localDeliveryProvider = _mapper.Map<CreateLocalDeliveryProvider, LocalDeliveryProvider>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await applicationDbContext.AddAsync(localDeliveryProvider);

            try
            {
                await applicationDbContext.SaveChangesAsync();

                foreach (var addressDistrictId in command.SupportedAddressDistrictIds)
                {
                    var request = new CreateLocalDeliveryProvider_AddressDistrictRequest()
                    {
                        LocalDeliveryProviderId = localDeliveryProvider.Id,
                        AddressDistrictId = addressDistrictId
                    };

                    var createLocalDeliveryProvider_AddressDistrictCommand = _mapper.Map(request, new CreateLocalDeliveryProvider_AddressDistrict(command.Identity));

                    await _mediator.Send(createLocalDeliveryProvider_AddressDistrictCommand);
                }
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.LOCALDELIVERYPROVIDER,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.LOCALDELIVERYPROVIDER,
                TransactionType = TransactionType.CREATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, localDeliveryProvider.Id),
            }));

            await _mediator.Publish(new ClearEntityCache<LocalDeliveryProviderResponse>());

            var localDeliveryProviderResponse = _mapper.Map<LocalDeliveryProviderResponse>(localDeliveryProvider);

            return localDeliveryProviderResponse;
        }

        #endregion
    }
}
