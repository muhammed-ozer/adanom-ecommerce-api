namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProduct_ProductCategoriesHandler : IRequestHandler<GetProduct_ProductCategories, IEnumerable<ProductCategoryResponse>>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProduct_ProductCategoriesHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ProductCategoryResponse>> Handle(GetProduct_ProductCategories command, CancellationToken cancellationToken)
        {
            var productCategoriesQuery = _applicationDbContext.Product_ProductCategory_Mappings
                .Where(e => e.ProductId == command.ProductId && e.Product.DeletedAtUtc == null)
                .Select(e => e.ProductCategory);

            var productCategoryResponses = _mapper.Map<IEnumerable<ProductCategoryResponse>>(await productCategoriesQuery.ToListAsync());

            return productCategoryResponses;
        }

        #endregion
    }
}
