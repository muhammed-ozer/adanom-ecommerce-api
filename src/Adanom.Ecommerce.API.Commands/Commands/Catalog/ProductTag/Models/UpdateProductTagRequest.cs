namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductTagRequest
    {
        public long Id { get; set; }

        public string Value { get; set; } = null!;
    }
}