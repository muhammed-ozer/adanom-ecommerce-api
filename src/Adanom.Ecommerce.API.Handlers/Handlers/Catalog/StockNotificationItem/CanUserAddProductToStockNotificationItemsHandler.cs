namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CanUserAddProductToStockNotificationItemsHandler : IRequestHandler<CanUserAddProductToStockNotificationItems, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public CanUserAddProductToStockNotificationItemsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CanUserAddProductToStockNotificationItems command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var userHasProductOnStockNotificationItems = await applicationDbContext.StockNotificationItems
                .AnyAsync(e => e.UserId == command.UserId &&
                               e.ProductId == command.ProductId);

            return !userHasProductOnStockNotificationItems;
         }

        #endregion
    }
}
