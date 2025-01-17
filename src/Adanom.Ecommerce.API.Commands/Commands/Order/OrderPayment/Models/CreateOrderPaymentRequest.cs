namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateOrderPaymentRequest
    {
        public long OrderId { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        public string? TransactionId { get; set; } = null!;

        public string? GatewayInitializationResponse { get; set; }
    }
}