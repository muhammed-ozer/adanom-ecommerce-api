namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductReviews : IRequest<PaginatedData<ProductReviewResponse>>
    {
        #region Ctor

        public GetProductReviews(GetProductReviewsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetProductReviewsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
