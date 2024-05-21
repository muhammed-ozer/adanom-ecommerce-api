using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Commands.Models
{
    public class OrderPaymentResponse : BaseResponseEntity<long>
    {
        public long OrderId { get; set; }

        public string TransactionId { get; set; } = null!;

        public string GatewayInitializationResponse { get; set; } = null!;

        public string? GatewayResponse { get; set; }

        public OrderResponse? Order { get; set; }
    }
}