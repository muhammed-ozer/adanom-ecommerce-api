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

            return await applicationDbContext.Product_ProductSKU_Mappings
                .AsNoTracking()
                .Where(e => e.Product.DeletedAtUtc == null && e.ProductId == command.Id)
                .Select(e => new
                {
                    Stock = e.ProductSKU.StockQuantity,
                    Reserved = applicationDbContext.StockReservations
                        .Where(r => r.ProductId == command.Id)
                        .Sum(r => r.Amount)
                })
                .Select(x => x.Stock - x.Reserved)
                .FirstOrDefaultAsync(cancellationToken);
        }

        #endregion
    }
}
