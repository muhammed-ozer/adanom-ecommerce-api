namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductResponse : BaseResponseEntity<long>
    {
        public ProductResponse()
        {
            ProductCategories = new List<ProductCategoryResponse>();
            ProductSpecificationAttributes = new List<ProductSpecificationAttributeResponse>();
            ProductTags = new List<ProductTagResponse>();
            Images = new List<ImageResponse>();
            ProductReviews = new List<ProductReviewResponse>();
        }

        public long? BrandId { get; set; }

        public long ProductSKUId { get; set; }

        public string Name { get; set; } = null!;

        public string UrlSlug { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsNew { get; set; }

        public bool IsInHighlights { get; set; }

        public double OverallReviewPoints { get; set; }

        public int DisplayOrder { get; set; }

        public BrandResponse? Brand { get; set; }

        public ProductSKU? ProductSKU { get; set; }

        public ICollection<ProductCategoryResponse> ProductCategories { get; set; }

        public ICollection<ProductSpecificationAttributeResponse> ProductSpecificationAttributes { get; set; }

        public ICollection<ProductTagResponse> ProductTags { get; set; }

        public ImageResponse? DefaultImage { get; set; }

        public ICollection<ImageResponse> Images { get; set; }

        public ICollection<ProductReviewResponse> ProductReviews { get; set; }
    }
}