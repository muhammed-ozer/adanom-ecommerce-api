using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreatePickUpStoreHandler : IRequestHandler<CreatePickUpStore, PickUpStoreResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreatePickUpStoreHandler(
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

        public async Task<PickUpStoreResponse?> Handle(CreatePickUpStore command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            if (!command.IsDefault)
            {
                var hasAnyOtherPickUpStore = await _applicationDbContext.PickUpStores
                    .AsNoTracking()
                    .AnyAsync(e => e.DeletedAtUtc == null);

                if (!hasAnyOtherPickUpStore)
                {
                    command.IsDefault = true;
                }
            }
            else
            {
                var currentDefaultPickUpStore = await _applicationDbContext.PickUpStores
                    .Where(e => e.DeletedAtUtc == null &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (currentDefaultPickUpStore != null)
                {
                    currentDefaultPickUpStore.IsDefault = false;
                }
            }

            var pickUpStore = _mapper.Map<CreatePickUpStore, PickUpStore>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await _applicationDbContext.AddAsync(pickUpStore);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.PICKUPSTORE,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.PICKUPSTORE,
                TransactionType = TransactionType.CREATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, pickUpStore.Id),
            }));

            await _mediator.Publish(new ClearEntityCache<PickUpStoreResponse>());

            var pickUpStoreResponse = _mapper.Map<PickUpStoreResponse>(pickUpStore);

            return pickUpStoreResponse;
        }

        #endregion
    }
}
