namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateProductReviewRequest
    {
        public long ProductId { get; set; }

        public byte Points { get; set; }

        public string? Comment { get; set; }
    }
}