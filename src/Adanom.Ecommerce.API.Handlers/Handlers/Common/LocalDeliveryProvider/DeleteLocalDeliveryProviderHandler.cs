using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteLocalDeliveryProviderHandler : IRequestHandler<DeleteLocalDeliveryProvider, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteLocalDeliveryProviderHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteLocalDeliveryProvider command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var localDeliveryProvider = await _applicationDbContext.LocalDeliveryProviders
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .SingleAsync();

            localDeliveryProvider.DeletedAtUtc = DateTime.UtcNow;
            localDeliveryProvider.DeletedByUserId = userId;

            if (localDeliveryProvider.IsDefault)
            {
                var randomLocalDeliveryProvider = await _applicationDbContext.LocalDeliveryProviders
                    .Where(e => e.DeletedAtUtc == null && e.Id != command.Id)
                    .FirstOrDefaultAsync();

                if (randomLocalDeliveryProvider != null)
                {
                    randomLocalDeliveryProvider.IsDefault = true;
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
                    EntityType = EntityType.LOCALDELIVERYPROVIDER,
                    TransactionType = TransactionType.DELETE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.LOCALDELIVERYPROVIDER,
                TransactionType = TransactionType.DELETE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, localDeliveryProvider.Id),
            }));

            return true;
        }

        #endregion
    }
}
