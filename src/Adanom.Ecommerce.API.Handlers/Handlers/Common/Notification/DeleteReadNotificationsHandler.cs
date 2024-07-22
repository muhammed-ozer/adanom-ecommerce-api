namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteReadNotificationsHandler : IRequestHandler<DeleteReadNotifications, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteReadNotificationsHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteReadNotifications command, CancellationToken cancellationToken)
        {
            var notifications = await _applicationDbContext.Notifications
                .Where(e => e.ReadAtUtc != null)
                .ToListAsync();

            _applicationDbContext.Notifications.RemoveRange(notifications);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = Guid.Empty,
                    EntityType = EntityType.NOTIFICATION,
                    TransactionType = TransactionType.DELETE,
                    Description = LogMessages.CustomerTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            return true;
        }

        #endregion
    }
}
