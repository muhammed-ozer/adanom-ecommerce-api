namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductSpecificationAttributeGroupRequest
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}