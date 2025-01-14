namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductHasStocksHandler : IRequestHandler<DoesProductHasStocks, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProductHasStocksHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProductHasStocks command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productStocks = await applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null && e.Id == command.ProductId)
                .Select(e => e.ProductSKU.StockQuantity)
                .SingleOrDefaultAsync();

            if (command.Amount != null)
            {
                return productStocks >= command.Amount.Value;
            }

            return productStocks != 0;
        }

        #endregion
    }
}
