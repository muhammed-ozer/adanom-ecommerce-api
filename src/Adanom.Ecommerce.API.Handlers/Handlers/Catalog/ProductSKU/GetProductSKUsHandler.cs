namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductSKUsHandler : IRequestHandler<GetProductSKUs, PaginatedData<ProductSKUResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductSKUsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<ProductSKUResponse>> Handle(GetProductSKUs command, CancellationToken cancellationToken)
        {
            var productSKUsQuery = _applicationDbContext.ProductSKUs
                   .AsNoTracking()
                   .Where(e => e.DeletedAtUtc == null &&
                               e.ProductId == command.Filter.ProductId);

            var totalCount = productSKUsQuery.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                productSKUsQuery = productSKUsQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var productSKUResponses = _mapper.Map<IEnumerable<ProductSKUResponse>>(await productSKUsQuery.ToListAsync());

            return new PaginatedData<ProductSKUResponse>(
                productSKUResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
