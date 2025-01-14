using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class MarkAsReadNotificationsHandler : IRequestHandler<MarkAsReadNotifications, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public MarkAsReadNotificationsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region INotificationHandler Members

        public async Task<bool> Handle(MarkAsReadNotifications command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            foreach (var id in command.Ids)
            {
                var notification = await applicationDbContext.Notifications
                    .Where(e => e.Id == id)
                    .SingleOrDefaultAsync();

                if (notification == null)
                {
                    continue;
                }

                notification.ReadAtUtc = DateTime.UtcNow;
                notification.ReadByUserId = userId;
            }

            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
