namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductPriceHandler : IRequestHandler<GetProductPrice, ProductPriceResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductPriceHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductPriceResponse?> Handle(GetProductPrice command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productPrice = await applicationDbContext.ProductPrices
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .SingleOrDefaultAsync();

            return _mapper.Map<ProductPriceResponse>(productPrice);
        }

        #endregion
    }
}
