namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductPriceResponse : BaseResponseEntity<long>
    {
        public long TaxCategoryId { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal? DiscountedPrice { get; set; }

        public TaxCategoryResponse? TaxCategory { get; set; }
    }
}