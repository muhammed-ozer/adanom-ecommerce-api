namespace Adanom.Ecommerce.API.Commands.Models
{
    public class OrderPaymentResponse : BaseResponseEntity<long>
    {
        public long OrderId { get; set; }

        public OrderPaymentTypeResponse OrderPaymentType { get; set; } = null!;

        public string? TransactionId { get; set; } = null!;

        public string? GatewayInitializationResponse { get; set; }

        public string? GatewayResponse { get; set; }

        public bool Paid { get; set; }

        public DateTime? PaidAtUtc { get; set; }
    }
}