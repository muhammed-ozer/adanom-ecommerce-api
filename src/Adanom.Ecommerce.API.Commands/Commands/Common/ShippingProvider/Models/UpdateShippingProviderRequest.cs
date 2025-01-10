namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateShippingProviderRequest
    {
        public long Id { get; set; }

        public string DisplayName { get; set; } = null!;

        public decimal MinimumOrderGrandTotal { get; set; }

        public decimal MinimumFreeShippingOrderGrandTotal { get; set; }

        public decimal FeeTotal { get; set; }

        public byte TaxRate { get; set; }

        public byte ShippingInDays { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }
    }
}