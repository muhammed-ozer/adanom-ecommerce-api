namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductReview : IRequest<ProductReviewResponse?>
    {
        #region Ctor

        public GetProductReview(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}