using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class ProductSpecificationAttribute : BaseEntity<long>
    {
        public ProductSpecificationAttribute()
        {
            Product_ProductSpecificationAttribute_Mappings = new List<Product_ProductSpecificationAttribute_Mapping>();
        }

        public long ProductSpecificationAttributeGroupId { get; set; }

        [StringLength(250)]
        public string Name { get; set; } = null!;

        [StringLength(250)]
        public string Value { get; set; } = null!;

        public int DisplayOrder { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public ProductSpecificationAttributeGroup SpecificationAttributeGroup { get; set; } = null!;

        public ICollection<Product_ProductSpecificationAttribute_Mapping> Product_ProductSpecificationAttribute_Mappings { get; set; }
    }
}
