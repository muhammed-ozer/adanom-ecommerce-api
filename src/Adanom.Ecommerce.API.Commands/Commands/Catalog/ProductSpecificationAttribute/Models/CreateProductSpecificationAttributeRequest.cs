namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateProductSpecificationAttributeRequest
    {
        public long ProductSpecificationAttributeGroupId { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}