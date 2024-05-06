namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductCategoryRequest
    {
        public long Id { get; set; }

        public long? ParentId { get; set; }

        public ProductCategoryLevel ProductCategoryLevel { get; set; }

        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}