using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class MarkAsReadNotificationsHandler : IRequestHandler<MarkAsReadNotifications, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public MarkAsReadNotificationsHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region INotificationHandler Members

        public async Task<bool> Handle(MarkAsReadNotifications command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            foreach (var id in command.Ids)
            {
                var notification = await _applicationDbContext.Notifications
                    .Where(e => e.Id == id)
                    .SingleOrDefaultAsync();

                if (notification == null)
                {
                    continue;
                }

                notification.ReadAtUtc = DateTime.UtcNow;
                notification.ReadByUserId = userId;
            }

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = Guid.Empty,
                    EntityType = EntityType.NOTIFICATION,
                    TransactionType = TransactionType.UPDATE,
                    Description = LogMessages.AdminTransaction.DatabaseTransactionHasFailed,
                }));
            }

            return true;
        } 

        #endregion
    }
}
