namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CheckoutResponse
    {
        public decimal SubTotal { get; set; }

        public decimal SubTotalDiscount { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal ShippingFeeSubTotal { get; set; }

        public decimal ShippingFeeTax { get; set; }

        public decimal? UserDefaultDiscountRateBasedDiscount { get; set; }

        public decimal GrandTotal { get; set; }

        public bool IsFreeShipping { get; set; }

        public ShoppingCartResponse? ShoppingCart { get; set; }
    }
}