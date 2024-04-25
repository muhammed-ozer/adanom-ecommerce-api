namespace Adanom.Ecommerce.API.Data.Models
{
    public class ShippingSettings : IBaseEntity<long>
    {
        public decimal MinimumFreeShippingTotalPrice { get; set; }

        public decimal FeeWithoutTax { get; set; }

        public decimal FeeTax { get; set; }

        public decimal FeeTotal { get; set; }

        public byte TaxRate { get; set; }

        public byte ShippingInDays { get; set; }
    }
}
