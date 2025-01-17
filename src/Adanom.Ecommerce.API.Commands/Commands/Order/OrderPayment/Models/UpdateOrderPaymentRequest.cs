namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateOrderPaymentRequest
    {
        public long Id { get; set; }

        public long OrderId { get; set; }

        public string? GatewayResponse { get; set; }

        public bool Paid { get; set; }

        public DateTime? PaidAtUtc { get; set; }
    }
}