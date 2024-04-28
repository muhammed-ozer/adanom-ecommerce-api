using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class ProductCategory : IBaseEntity<long>
    {
        public ProductCategory()
        {
            Children = new List<ProductCategory>();
            Product_ProductCategory_Mappings = new List<Product_ProductCategory_Mapping>();
        }

        public long? ParentId { get; set; }

        public ProductCategoryLevel ProductCategoryLevel { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(150)]
        public string UrlSlug { get; set; } = null!;

        public int DisplayOrder { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public ProductCategory? Parent { get; set; }

        public ICollection<ProductCategory> Children { get; set; }

        public ICollection<Product_ProductCategory_Mapping> Product_ProductCategory_Mappings { get; set; }
    }
}
