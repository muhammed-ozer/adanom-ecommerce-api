namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateProductRequest
    {
        public long ProductCategoryId { get; set; }

        public long? BrandId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsNew { get; set; }

        public int DisplayOrder { get; set; }

        public CreateProductSKURequest CreateProductSKURequest { get; set; } = null!;
    }
}