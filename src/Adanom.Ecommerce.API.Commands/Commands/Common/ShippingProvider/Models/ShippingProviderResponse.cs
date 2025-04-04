namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ShippingProviderResponse : BaseResponseEntity<long>
    {
        public string DisplayName { get; set; } = null!;

        public decimal MinimumOrderGrandTotal { get; set; }

        public decimal MinimumFreeShippingOrderGrandTotal { get; set; }

        public decimal FeeTotal { get; set; }

        public byte TaxRate { get; set; }

        public byte ShippingInDays { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        public ImageResponse? Logo { get; set; }
    }
}