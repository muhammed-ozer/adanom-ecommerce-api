namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductHasStocksHandler : IRequestHandler<DoesProductHasStocks, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductHasStocksHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProductHasStocks command, CancellationToken cancellationToken)
        {
            var productStocks = await _applicationDbContext.Products
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
