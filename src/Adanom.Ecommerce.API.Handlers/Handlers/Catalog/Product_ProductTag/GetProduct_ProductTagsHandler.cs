using System.Collections.Concurrent;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProduct_ProductTagsHandler : IRequestHandler<GetProduct_ProductTags, IEnumerable<ProductTagResponse>>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, ProductTagResponse> _cache = new();

        #endregion

        #region Ctor

        public GetProduct_ProductTagsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ProductTagResponse>> Handle(GetProduct_ProductTags command, CancellationToken cancellationToken)
        {
            var productTagsQuery = _applicationDbContext.Product_ProductTag_Mappings
                .Where(e => e.ProductId == command.ProductId && e.Product.DeletedAtUtc == null)
                .Select(e => e.ProductTag);

            var productTagResponses = _mapper.Map<IEnumerable<ProductTagResponse>>(await productTagsQuery.ToListAsync());

            return productTagResponses;
        }

        #endregion
    }
}
