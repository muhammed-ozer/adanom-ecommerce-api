namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductPriceTaxCategoryRequest
    {
        public long Id { get; set; }

        public long TaxCategoryId { get; set; }
    }
}