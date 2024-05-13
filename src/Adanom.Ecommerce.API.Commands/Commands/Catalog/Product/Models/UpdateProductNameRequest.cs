namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductNameRequest
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;
    }
}