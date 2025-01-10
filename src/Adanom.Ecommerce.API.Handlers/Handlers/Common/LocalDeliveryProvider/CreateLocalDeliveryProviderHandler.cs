using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateLocalDeliveryProviderHandler : IRequestHandler<CreateLocalDeliveryProvider, LocalDeliveryProviderResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateLocalDeliveryProviderHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<LocalDeliveryProviderResponse?> Handle(CreateLocalDeliveryProvider command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            if (!command.IsDefault)
            {
                var hasAnyOtherLocalDeliveryProvider = await _applicationDbContext.LocalDeliveryProviders
                    .AsNoTracking()
                    .AnyAsync(e => e.DeletedAtUtc == null);

                if (!hasAnyOtherLocalDeliveryProvider)
                {
                    command.IsDefault = true;
                }
            }
            else
            {
                var currentDefaultLocalDeliveryProvider = await _applicationDbContext.LocalDeliveryProviders
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

            foreach (var addressDistrictId in command.SupportedAddressDistrictIds) 
            {
                var addressDistrict = await _applicationDbContext.AddressDistricts
                    .Where(e => e.Id == addressDistrictId)
                    .SingleAsync();

                localDeliveryProvider.SupportedAddressDistricts.Add(addressDistrict);
            }

            await _applicationDbContext.AddAsync(localDeliveryProvider);

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
