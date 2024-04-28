using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(ReturnRequestNumber), IsUnique = true)]
    public class ReturnRequest : BaseEntity<long>
    {
        public ReturnRequest()
        {
            Items = new List<ReturnRequestItem>();
        }

        public long OrderId { get; set; }

        public long? ShippingProviderId { get; set; }

        public long? PickUpStoreId { get; set; }

        public ReturnRequestStatusType ReturnRequestStatusType { get; set; }

        public DeliveryType DeliveryType { get; set; }

        [StringLength(25)]
        public string ReturnRequestNumber { get; set; } = null!;

        public decimal GrandTotal { get; set; }

        [StringLength(250)]
        public string? ShippingTransactionCode { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Order Order { get; set; } = null!;

        public ShippingProvider? ShippingProvider { get; set; }

        public PickUpStore? PickUpStore { get; set; }

        public ICollection<ReturnRequestItem> Items { get; set; }
    }
}
