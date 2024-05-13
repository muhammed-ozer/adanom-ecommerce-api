namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductPrice_PriceRequest
    {
        public long Id { get; set; }

        public decimal OriginalPrice { get; set; }
    }
}