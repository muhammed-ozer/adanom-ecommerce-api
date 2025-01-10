using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class LocalDeliveryProvider : BaseEntity<long>
    {
        [StringLength(250)]
        public string DisplayName { get; set; } = null!;

        public decimal MinimumOrderGrandTotal { get; set; }

        public decimal MinimumFreeDeliveryOrderGrandTotal { get; set; }

        public decimal FeeTotal { get; set; }

        public byte TaxRate { get; set; }

        public byte DeliveryInHours { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public ICollection<LocalDeliveryProvider_AddressDistrict_Mapping> LocalDeliveryProvider_AddressDistrict_Mappings { get; set; } = [];
    }
}
