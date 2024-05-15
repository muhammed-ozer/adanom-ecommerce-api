namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetProductReviewsFilter
    {
        public bool? IsApproved { get; set; }

        public long? ProductId { get; set; }

        [DefaultValue(GetProductReviewsOrderByEnum.APPROVED_AT_ASC)]
        public GetProductReviewsOrderByEnum? OrderBy { get; set; }
    }
}