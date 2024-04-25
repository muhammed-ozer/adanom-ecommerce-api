using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class ProductAttributeOption : IBaseEntity<long>
    {
        public long ProductAttributeId { get; set; }

        [StringLength(250)]
        public string Value { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public ProductAttribute ProductAttribute { get; set; } = null!;
    }
}
