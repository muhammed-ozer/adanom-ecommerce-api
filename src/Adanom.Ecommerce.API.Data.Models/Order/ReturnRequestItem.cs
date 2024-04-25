using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class ReturnRequestItem : IBaseEntity<long>
    {
        public long ReturnRequestId { get; set; }

        public long OrderItemId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        [StringLength(20)]
        public string AmountUnit { get; set; } = null!;

        public byte TaxRate { get; set; }

        public decimal Total { get; set; }

        [StringLength(100)]
        public string Description { get; set; } = null!;

        public ReturnRequest ReturnRequest { get; set; } = null!;
    }
}
