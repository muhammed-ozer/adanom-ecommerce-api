namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateShippingProviderRequest
    {
        public string DisplayName { get; set; } = null!;

        public decimal MinimumFreeShippingTotalPrice { get; set; }

        public decimal FeeWithoutTax { get; set; }

        public decimal FeeTax { get; set; }

        public decimal FeeTotal { get; set; }

        public byte TaxRate { get; set; }

        public byte ShippingInDays { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }
    }
}