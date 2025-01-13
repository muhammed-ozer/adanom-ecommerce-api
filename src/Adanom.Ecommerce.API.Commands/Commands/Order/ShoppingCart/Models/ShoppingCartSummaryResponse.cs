namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ShoppingCartSummaryResponse : BaseResponseEntity<long>
    {
        public decimal SubTotal { get; set; }

        public decimal? TotalDiscount { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal? UserDefaultDiscountRateBasedDiscount { get; set; }

        public decimal? DiscountByOrderPaymentType { get; set; }

        public decimal GrandTotal { get; set; }
    }
}