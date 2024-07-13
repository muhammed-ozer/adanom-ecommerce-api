namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CanUserAddProductToStockNotificationItemsHandler : IRequestHandler<CanUserAddProductToStockNotificationItems, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public CanUserAddProductToStockNotificationItemsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CanUserAddProductToStockNotificationItems command, CancellationToken cancellationToken)
        {
            var userHasProductOnStockNotificationItems = await _applicationDbContext.StockNotificationItems
                .AnyAsync(e => e.UserId == command.UserId &&
                               e.ProductId == command.ProductId);

            return !userHasProductOnStockNotificationItems;
         }

        #endregion
    }
}
