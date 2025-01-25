namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductSKUHandler : IRequestHandler<GetProductSKU, ProductSKUResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductSKUHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductSKUResponse?> Handle(GetProductSKU command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var result = await applicationDbContext.ProductSKUs
                .Where(e => e.DeletedAtUtc == null &&
                    (command.Code.IsNotNullOrEmpty() ? e.Code == command.Code : e.Id == command.Id))
                .Select(sku => new
                {
                    ProductSKU = sku,
                    ReservedStock = applicationDbContext.StockReservations
                        .Where(r => r.ProductId == applicationDbContext.Product_ProductSKU_Mappings
                            .Where(m => m.ProductSKUId == sku.Id)
                            .Select(m => m.ProductId)
                            .FirstOrDefault())
                        .Sum(e => e.Amount)
                })
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (result != null)
            {
                result.ProductSKU.StockQuantity -= result.ReservedStock;
            }

            return _mapper.Map<ProductSKUResponse>(result?.ProductSKU ?? null);
        }

        #endregion
    }
}
