namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateProduct_ProductTagRequest
    {
        public long ProductId { get; set; }

        public string ProductTag_Value { get; set; } = null!;
    }
}