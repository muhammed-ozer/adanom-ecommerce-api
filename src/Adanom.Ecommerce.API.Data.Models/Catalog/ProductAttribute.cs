using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class ProductAttribute : IBaseEntity<long>
    {
        public ProductAttribute()
        {
            ProductAttributeOptions = new List<ProductAttributeOption>();
        }

        [StringLength(250)]
        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public ICollection<ProductAttributeOption> ProductAttributeOptions { get; set; }
    }
}
