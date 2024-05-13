namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductResponse : BaseResponseEntity<long>
    {
        public ProductResponse()
        {
            ProductSKUs = new List<ProductSKUResponse>();
            ProductCategories = new List<ProductCategoryResponse>();
            ProductSpecificationAttributes = new List<ProductSpecificationAttributeResponse>();
            ProductTags = new List<ProductTagResponse>();
        }

        public long? BrandId { get; set; }

        public string Name { get; set; } = null!;

        public string UrlSlug { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsNew { get; set; }

        public double OverallReviewPoints { get; set; }

        public int DisplayOrder { get; set; }

        public BrandResponse? Brand { get; set; }

        public ICollection<ProductSKUResponse> ProductSKUs { get; set; }

        public ICollection<ProductCategoryResponse> ProductCategories { get; set; }

        public ICollection<ProductSpecificationAttributeResponse> ProductSpecificationAttributes { get; set; }

        public ICollection<ProductTagResponse> ProductTags { get; set; }
    }
}