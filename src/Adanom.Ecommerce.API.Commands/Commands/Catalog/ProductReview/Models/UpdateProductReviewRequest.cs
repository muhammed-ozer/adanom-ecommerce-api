namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductReviewRequest
    {
        public long Id { get; set; }

        public bool IsApproved { get; set; }

        public int DisplayOrder { get; set; }
    }
}