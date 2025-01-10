namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateLocalDeliveryProviderRequest
    {
        public string DisplayName { get; set; } = null!;

        public decimal MinimumOrderGrandTotal { get; set; }

        public decimal MinimumFreeDeliveryOrderGrandTotal { get; set; }

        public decimal FeeTotal { get; set; }

        public byte TaxRate { get; set; }

        public byte DeliveryInHours { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        public ICollection<long> SupportedAddressDistrictIds { get; set; } = new List<long>();
    }
}