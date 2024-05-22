using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeletePickUpStoreHandler : IRequestHandler<DeletePickUpStore, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeletePickUpStoreHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeletePickUpStore command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var pickUpStore = await _applicationDbContext.PickUpStores
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .SingleAsync();

            pickUpStore.DeletedAtUtc = DateTime.UtcNow;
            pickUpStore.DeletedByUserId = userId;

            if (pickUpStore.IsDefault)
            {
                var randomPickUpStore = await _applicationDbContext.PickUpStores
                    .Where(e => e.DeletedAtUtc == null && e.Id != command.Id)
                    .FirstOrDefaultAsync();

                if (randomPickUpStore != null)
                {
                    randomPickUpStore.IsDefault = true;
                }
            }

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
                    TransactionType = TransactionType.DELETE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.PICKUPSTORE,
                TransactionType = TransactionType.DELETE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, pickUpStore.Id),
            }));

            return true;
        }

        #endregion
    }
}
