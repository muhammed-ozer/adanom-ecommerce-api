namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProduct_ProductSpecificationAttributesHandler : IRequestHandler<GetProduct_ProductSpecificationAttributes, IEnumerable<ProductSpecificationAttributeResponse>>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProduct_ProductSpecificationAttributesHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ProductSpecificationAttributeResponse>> Handle(GetProduct_ProductSpecificationAttributes command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productSpecificationAttributesQuery = applicationDbContext.Product_ProductSpecificationAttribute_Mappings
                .Where(e => e.ProductId == command.ProductId && e.Product.DeletedAtUtc == null)
                .Select(e => e.ProductSpecificationAttribute);

            var productSpecificationAttributeResponses = _mapper
                .Map<IEnumerable<ProductSpecificationAttributeResponse>>(await productSpecificationAttributesQuery.ToListAsync());

            return productSpecificationAttributeResponses;
        }

        #endregion
    }
}
