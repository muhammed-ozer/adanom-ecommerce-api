namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductSKUByProductIdHandler : IRequestHandler<GetProductSKUByProductId, ProductSKUResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductSKUByProductIdHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductSKUResponse?> Handle(GetProductSKUByProductId command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var result = await applicationDbContext.Product_ProductSKU_Mappings
                .Where(e => e.ProductId == command.Id && e.Product.DeletedAtUtc == null)
                .Select(e => new
                {
                    e.ProductSKU,
                    ReservedStock = applicationDbContext.StockReservations
                        .Where(r => r.ProductId == applicationDbContext.Product_ProductSKU_Mappings
                            .Where(m => m.ProductSKUId == e.ProductSKU.Id)
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

            return _mapper.Map<ProductSKUResponse>(result?.ProductSKU);
        }

        #endregion
    }
}
