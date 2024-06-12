namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateProductCategoryRequest
    {
        public long? ParentId { get; set; }

        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}