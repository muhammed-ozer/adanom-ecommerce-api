namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteReadNotificationsHandler : IRequestHandler<DeleteReadNotifications, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteReadNotificationsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteReadNotifications command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var notifications = await applicationDbContext.Notifications
                .Where(e => e.ReadAtUtc != null)
                .ToListAsync();

            applicationDbContext.Notifications.RemoveRange(notifications);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
