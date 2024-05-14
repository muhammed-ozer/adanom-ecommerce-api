namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductCategoryResponse : BaseResponseEntity<long>
    {
        public ProductCategoryResponse()
        {
            Children = new List<ProductCategoryResponse>();
        }

        public long? ParentId { get; set; }

        public ProductCategoryLevel ProductCategoryLevel { get; set; }

        public string Name { get; set; } = null!;

        public string UrlSlug { get; set; } = null!;

        public int DisplayOrder { get; set; }

        public ProductCategoryResponse? Parent { get; set; }

        public ICollection<ProductCategoryResponse> Children { get; set; }

        public MetaInformation? MetaInformation { get; set; }
    }
}