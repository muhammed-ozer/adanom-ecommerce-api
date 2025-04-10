namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CheckoutResponse
    {
        public decimal SubTotal { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal ShippingFeeTotal { get; set; }

        public decimal ShippingFeeTax { get; set; }

        public decimal? UserDefaultDiscountRateBasedDiscount { get; set; }

        public decimal? DiscountByOrderPaymentType { get; set; }

        public decimal GrandTotal { get; set; }

        public bool IsFreeShipping { get; set; }

        public bool HasPriceChanges { get; set; }

        public bool HasProductDeleted { get; set; }

        public bool HasNoItem { get; set; }

        public bool HasStocksChanges { get; set; }

        public bool CanCreateOrder { get; set; } = true;

        public ICollection<ShoppingCartItemResponse> Items { get; set; } = [];

        public ICollection<string> Errors { get; set; } = [];

        public string DistanceSellingContractHtmlContent { get; set; } = null!;

        public string PreliminaryInformationFormHtmlContent { get; set; } = null!;
    }
}