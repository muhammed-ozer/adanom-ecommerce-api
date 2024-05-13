namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductAttributeRequest
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}