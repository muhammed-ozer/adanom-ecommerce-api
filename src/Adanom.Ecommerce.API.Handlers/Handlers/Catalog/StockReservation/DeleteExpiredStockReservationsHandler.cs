namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteExpiredStockReservationsHandler : INotificationHandler<DeleteExpiredStockReservations>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DeleteExpiredStockReservationsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region INotificationHandler Members

        public async Task Handle(DeleteExpiredStockReservations command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.StockReservations
               .Where(e => e.ExpiresAtUtc < DateTime.UtcNow)
               .ExecuteDeleteAsync();
        } 

        #endregion
    }
}
