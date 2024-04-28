using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class ShippingProvider : BaseEntity<long>
    {
        public long ShippingSettingsId { get; set; }

        [StringLength(250)]
        public string DisplayName { get; set; } = null!;

        [StringLength(500)]
        public string LogoPath { get; set; } = null!;

        public bool IsActive { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreateByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public ShippingSettings ShippingSettings { get; set; } = null!;
    }
}
