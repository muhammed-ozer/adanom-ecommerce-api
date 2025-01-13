namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CalculateShippingForCheckoutAndOrderResponse : BaseResponseEntity<long>
    {
        public ShippingProviderResponse? ShippingProvider { get; set; }

        public bool IsFreeShipping { get; set; }

        public decimal ShippingFeeTotal { get; set; }

        public decimal ShippingFeeTax { get; set; }

        public bool IsShippable { get; set; }

        public string? ErrorMessage { get; set; }
    }
}