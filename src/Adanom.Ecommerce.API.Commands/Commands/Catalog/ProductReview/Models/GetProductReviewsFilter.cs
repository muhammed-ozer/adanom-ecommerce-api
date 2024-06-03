namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetProductReviewsFilter
    {
        public bool? IsApproved { get; set; }

        public long? ProductId { get; set; }

        [DefaultValue(GetProductReviewsOrderByEnum.CREATED_AT_DESC)]
        public GetProductReviewsOrderByEnum? OrderBy { get; set; }
    }
}