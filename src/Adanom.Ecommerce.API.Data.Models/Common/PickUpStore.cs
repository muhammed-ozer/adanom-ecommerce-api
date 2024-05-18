using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class PickUpStore : BaseEntity<long>
    {
        [StringLength(250)]
        public string DisplayName { get; set; } = null!;

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        [StringLength(500)]
        public string Address { get; set; } = null!;

        [StringLength(10)]
        public string PhoneNumber { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }
    }
}
