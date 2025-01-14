namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductPriceByProductIdHandler : IRequestHandler<GetProductPriceByProductId, ProductPriceResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductPriceByProductIdHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductPriceResponse?> Handle(GetProductPriceByProductId command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productPrice = await applicationDbContext.Products
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .Select(e => e.ProductSKU.ProductPrice)
                .SingleOrDefaultAsync();
           
            return _mapper.Map<ProductPriceResponse>(productPrice);
        } 

        #endregion
    }
}
