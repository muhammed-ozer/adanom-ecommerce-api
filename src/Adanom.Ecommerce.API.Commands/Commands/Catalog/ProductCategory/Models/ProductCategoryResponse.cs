namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductCategoryResponse : BaseResponseEntity<long>
    {
        public ProductCategoryResponse()
        {
            Children = new List<ProductCategoryResponse>();
            Images = new List<ImageResponse>();
        }

        public long? ParentId { get; set; }

        public string Name { get; set; } = null!;

        public string UrlSlug { get; set; } = null!;

        public int DisplayOrder { get; set; }

        public ProductCategoryResponse? Parent { get; set; }

        public ICollection<ProductCategoryResponse> Children { get; set; }

        public ICollection<ImageResponse> Images { get; set; }
    }
}