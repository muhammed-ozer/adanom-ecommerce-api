namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProduct_ProductTagsHandler : IRequestHandler<GetProduct_ProductTags, IEnumerable<ProductTagResponse>>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProduct_ProductTagsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ProductTagResponse>> Handle(GetProduct_ProductTags command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productTagsQuery = applicationDbContext.Product_ProductTag_Mappings
                .Where(e => e.ProductId == command.ProductId && e.Product.DeletedAtUtc == null)
                .Select(e => e.ProductTag);

            var productTagResponses = _mapper.Map<IEnumerable<ProductTagResponse>>(await productTagsQuery.ToListAsync());

            return productTagResponses;
        }

        #endregion
    }
}
