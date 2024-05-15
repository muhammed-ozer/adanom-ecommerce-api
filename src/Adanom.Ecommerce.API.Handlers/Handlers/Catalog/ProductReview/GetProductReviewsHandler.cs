namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductReviewsHandler : IRequestHandler<GetProductReviews, PaginatedData<ProductReviewResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductReviewsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<ProductReviewResponse>> Handle(GetProductReviews command, CancellationToken cancellationToken)
        {
            var productReviewsQuery = _applicationDbContext.ProductReviews.AsNoTracking();

            var totalCount = productReviewsQuery.Count();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (command.Filter.ProductId != null)
                {
                    productReviewsQuery = productReviewsQuery.Where(e => e.ProductId == command.Filter.ProductId);
                }

                if (command.Filter.IsApproved != null)
                {
                    productReviewsQuery = productReviewsQuery.Where(e => e.IsApproved);
                }

                #endregion

                #region Apply ordering

                productReviewsQuery = command.Filter.OrderBy switch
                {
                    GetProductReviewsOrderByEnum.DISPLAY_ORDER_ASC =>
                        productReviewsQuery.OrderBy(e => e.DisplayOrder),
                    GetProductReviewsOrderByEnum.DISPLAY_ORDER_DESC =>
                        productReviewsQuery.OrderByDescending(e => e.DisplayOrder),
                    GetProductReviewsOrderByEnum.POINTS_ASC =>
                        productReviewsQuery.OrderBy(e => e.Points),
                    GetProductReviewsOrderByEnum.POINTS_DESC =>
                        productReviewsQuery.OrderByDescending(e => e.Points),
                    GetProductReviewsOrderByEnum.CREATED_AT_ASC =>
                        productReviewsQuery.OrderBy(e => e.CreatedAtUtc),
                    GetProductReviewsOrderByEnum.CREATED_AT_DESC =>
                        productReviewsQuery.OrderByDescending(e => e.CreatedAtUtc),
                    GetProductReviewsOrderByEnum.APPROVED_AT_DESC =>
                        productReviewsQuery.OrderByDescending(e => e.ApprovedAtUtc),
                    _ =>
                        productReviewsQuery.OrderBy(e => e.ApprovedAtUtc)
                };

                #endregion
            }

            #region Apply pagination

            if (command.Pagination is not null)
            {
                productReviewsQuery = productReviewsQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var productReviewResponses = _mapper.Map<IEnumerable<ProductReviewResponse>>(await productReviewsQuery.ToListAsync());

            return new PaginatedData<ProductReviewResponse>(
                productReviewResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
