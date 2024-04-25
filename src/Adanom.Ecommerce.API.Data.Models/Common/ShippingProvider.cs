using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class ShippingProvider : IBaseEntity<long>
    {
        [StringLength(250)]
        public string DisplayName { get; set; } = null!;

        [StringLength(500)]
        public string LogoPath { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreateByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }
    }
}
