using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class OrderPayment : BaseEntity<long>
    {
        public long OrderId { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        [StringLength(100)]
        public string? TransactionId { get; set; } = null!;

        [StringLength(5000)]
        public string? GatewayInitializationResponse { get; set; }

        [StringLength(5000)]
        public string? GatewayResponse { get; set; }

        public bool Paid { get; set; }

        public DateTime? PaidAtUtc { get; set; }

        public Order Order { get; set; } = null!;
    }
}
