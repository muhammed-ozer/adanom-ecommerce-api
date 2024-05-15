using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class ProductReview : BaseEntity<long>
    {
        public long ProductId { get; set; }

        public Guid UserId { get; set; }

        public byte Points { get; set; }

        [StringLength(500)]
        public string? Comment { get; set; }

        public bool IsApproved { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? ApprovedAtUtc { get; set; }

        public Guid? ApprovedByUserId { get; set; }

        public Product Product { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
