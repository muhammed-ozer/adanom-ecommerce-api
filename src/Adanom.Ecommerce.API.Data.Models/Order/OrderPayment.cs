using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class OrderPayment : BaseEntity<long>
    {
        public long OrderId { get; set; }

        [StringLength(100)]
        public string TransactionId { get; set; } = null!;

        [StringLength(1000)]
        public string GatewayInitializationResponse { get; set; } = null!;

        [StringLength(1000)]
        public string? GatewayResponse { get; set; }

        public Order Order { get; set; } = null!;
    }
}
