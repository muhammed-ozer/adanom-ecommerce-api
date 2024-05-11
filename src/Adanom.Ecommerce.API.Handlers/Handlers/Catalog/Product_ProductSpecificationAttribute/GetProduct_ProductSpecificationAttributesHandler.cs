namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProduct_ProductSpecificationAttributesHandler : IRequestHandler<GetProduct_ProductSpecificationAttributes, IEnumerable<ProductSpecificationAttributeResponse>>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProduct_ProductSpecificationAttributesHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ProductSpecificationAttributeResponse>> Handle(GetProduct_ProductSpecificationAttributes command, CancellationToken cancellationToken)
        {
            var productSpecificationAttributesQuery = _applicationDbContext.Product_ProductSpecificationAttribute_Mappings
                .Where(e => e.ProductId == command.ProductId && e.Product.DeletedAtUtc == null)
                .Select(e => e.ProductSpecificationAttribute);

            var productSpecificationAttributeResponses = _mapper
                .Map<IEnumerable<ProductSpecificationAttributeResponse>>(await productSpecificationAttributesQuery.ToListAsync());

            return productSpecificationAttributeResponses;
        }

        #endregion
    }
}
