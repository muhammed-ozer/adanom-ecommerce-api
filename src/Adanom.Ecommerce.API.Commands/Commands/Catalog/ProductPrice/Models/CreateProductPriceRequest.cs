namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateProductPriceRequest
    {
        public long TaxCategoryId { get; set; }

        public decimal OriginalPrice { get; set; }
    }
}