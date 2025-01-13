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
            ProductSKU? productSKU = null;

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (command.Code.IsNotNullOrEmpty())
            {
                productSKU = await applicationDbContext.ProductSKUs
                    .Where(e => e.DeletedAtUtc == null && e.Code == command.Code)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            else
            {
                productSKU = await applicationDbContext.ProductSKUs
                    .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }

            return _mapper.Map<ProductSKUResponse>(productSKU);
        }

        #endregion
    }
}
