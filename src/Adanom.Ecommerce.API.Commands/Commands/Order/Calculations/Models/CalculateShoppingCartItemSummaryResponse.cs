namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CalculateShoppingCartItemSummaryResponse : BaseResponseEntity<long>
    {
        public byte TaxRate { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal SubTotal { get; set; }

        public decimal? DiscountTotal { get; set; }

        public decimal? UserDefaultDiscountRateBasedDiscount { get; set; }

        public decimal? DiscountByOrderPaymentType { get; set; }
    }
}