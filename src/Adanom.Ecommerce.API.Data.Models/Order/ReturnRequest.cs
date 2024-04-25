using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(ReturnRequestNumber), IsUnique = true)]
    public class ReturnRequest : IBaseEntity<long>
    {
        public ReturnRequest()
        {
            Items = new List<ReturnRequestItem>();
        }

        public long OrderId { get; set; }

        public long ShippingProviderId { get; set; }

        public ReturnRequestStatusType ReturnRequestStatusType { get; set; }

        [StringLength(25)]
        public string ReturnRequestNumber { get; set; } = null!;

        public decimal GrandTotal { get; set; }

        [StringLength(250)]
        public string ShippingTransactionCode { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }

        public Order Order { get; set; } = null!;

        public ShippingProvider ShippingProvider { get; set; } = null!;

        public ICollection<ReturnRequestItem> Items { get; set; }
    }
}
