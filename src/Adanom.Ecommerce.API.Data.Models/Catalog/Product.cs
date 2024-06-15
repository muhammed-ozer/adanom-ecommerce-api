using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(UrlSlug), IsUnique = true)]
    public class Product : BaseEntity<long>
    {
        public Product()
        {
            ProductReviews = new List<ProductReview>();
            Product_ProductCategory_Mappings = new List<Product_ProductCategory_Mapping>();
            Product_ProductSpecificationAttribute_Mappings = new List<Product_ProductSpecificationAttribute_Mapping>();
        }

        public long ProductSKUId { get; set; }

        public long? BrandId { get; set; }

        [StringLength(250)]
        public string Name { get; set; } = null!;

        [StringLength(400)]
        public string UrlSlug { get; set; } = null!;

        [StringLength(4000)]
        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsNew { get; set; }

        public double OverallReviewPoints { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public ProductSKU ProductSKU { get; set; } = null!;

        public ICollection<ProductReview> ProductReviews { get; set; }

        public Brand? Brand { get; set; }

        public ICollection<Product_ProductCategory_Mapping> Product_ProductCategory_Mappings { get; set; }

        public ICollection<Product_ProductSpecificationAttribute_Mapping> Product_ProductSpecificationAttribute_Mappings { get; set; }
    }
}
