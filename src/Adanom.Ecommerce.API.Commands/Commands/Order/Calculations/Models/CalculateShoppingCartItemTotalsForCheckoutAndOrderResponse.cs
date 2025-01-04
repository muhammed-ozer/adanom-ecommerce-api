namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CalculateShoppingCartItemTotalsForCheckoutAndOrderResponse : BaseResponseEntity<long>
    {
        public StockUnitTypeResponse StockUnitType { get; set; } = null!;

        public ProductSKUResponse ProductSKU { get; set; } = null!;

        public byte TaxRate { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal SubTotal { get; set; }

        public decimal? SubDiscountedTotal { get; set; }

        public decimal? UserDefaultDiscountRateBasedDiscount { get; set; }
    }
}