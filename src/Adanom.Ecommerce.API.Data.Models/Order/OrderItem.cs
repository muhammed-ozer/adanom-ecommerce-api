using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class OrderItem : BaseEntity<long>
    {
        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public decimal TaxExcludedPrice { get; set; }

        public int Amount { get; set; }

        [StringLength(20)]
        public string AmountUnit { get; set; } = null!;

        public byte TaxRate { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal DiscountTotal { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public Order Order { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}
