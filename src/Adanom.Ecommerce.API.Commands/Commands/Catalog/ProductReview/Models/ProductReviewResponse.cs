namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductReviewResponse : BaseResponseEntity<long>
    {
        public long ProductId { get; set; }

        public Guid UserId { get; set; }

        public byte Points { get; set; }

        public string? Comment { get; set; }

        public bool IsApproved { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? ApprovedAtUtc { get; set; }

        public Guid? ApprovedByUserId { get; set; }
    }
}