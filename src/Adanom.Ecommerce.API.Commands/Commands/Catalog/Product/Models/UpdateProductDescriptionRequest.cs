namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductDescriptionRequest
    {
        public long Id { get; set; }

        public string? Description { get; set; }
    }
}