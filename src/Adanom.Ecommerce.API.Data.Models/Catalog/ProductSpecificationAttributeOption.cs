using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class ProductSpecificationAttributeOption : BaseEntity<long>
    {
        public ProductSpecificationAttributeOption()
        {
            Product_ProductSpecificationAttributeOption_Mappings = new List<Product_ProductSpecificationAttributeOption_Mapping>();
        }

        public long ProductSpecificationAttributeId { get; set; }

        [StringLength(250)]
        public string Value { get; set; } = null!;

        public int DisplayOrder { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public ProductSpecificationAttribute SpecificationAttribute { get; set; } = null!;

        public ICollection<Product_ProductSpecificationAttributeOption_Mapping> Product_ProductSpecificationAttributeOption_Mappings { get; set; }
    }
}
