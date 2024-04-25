using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(UrlSlug), IsUnique = true)]
    public class Product : IBaseEntity<long>
    {
        public Product()
        {
            ProductReviews = new List<ProductReview>();
            Product_ProductCategory_Mappings = new List<Product_ProductCategory_Mapping>();
            Product_Image_Mappings = new List<Product_Image_Mapping>();
            Product_MetaInformation_Mappings = new List<Product_MetaInformation_Mapping>();
            Product_ProductSpecificationAttributeOption_Mappings = new List<Product_ProductSpecificationAttributeOption_Mapping>();
            ProductSKUs = new List<ProductSKU>();
        }

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

        public ICollection<ProductReview> ProductReviews { get; set; }

        public Brand? Brand { get; set; }

        public ICollection<ProductSKU> ProductSKUs { get; set; }

        public ICollection<Product_ProductCategory_Mapping> Product_ProductCategory_Mappings { get; set; }

        public ICollection<Product_Image_Mapping> Product_Image_Mappings { get; set; }

        public ICollection<Product_MetaInformation_Mapping> Product_MetaInformation_Mappings { get; set; }

        public ICollection<Product_ProductSpecificationAttributeOption_Mapping> Product_ProductSpecificationAttributeOption_Mappings { get; set; }
    }
}
