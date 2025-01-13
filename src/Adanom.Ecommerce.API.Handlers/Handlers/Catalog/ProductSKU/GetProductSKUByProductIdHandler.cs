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

            var productSKU = await applicationDbContext.Products
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .Select(e => e.ProductSKU)
                .SingleOrDefaultAsync();
           
            return _mapper.Map<ProductSKUResponse>(productSKU);
        } 

        #endregion
    }
}
