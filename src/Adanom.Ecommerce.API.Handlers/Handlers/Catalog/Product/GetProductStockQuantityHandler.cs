namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductStockQuantityHandler : IRequestHandler<GetProductStockQuantity, int>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public GetProductStockQuantityHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<int> Handle(GetProductStockQuantity command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productStockQuantity = await applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .Select(e => e.ProductSKU.StockQuantity)
                .SingleOrDefaultAsync();

            var reservedStocks = await applicationDbContext.StockReservations
                .Where(e => e.ProductId == command.Id)
                .SumAsync(e => e.Amount);

            var stocks = productStockQuantity - reservedStocks;

            return stocks;
        } 

        #endregion
    }
}
