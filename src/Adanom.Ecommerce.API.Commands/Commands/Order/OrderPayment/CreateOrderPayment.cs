using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateOrderPayment : IRequest<bool>
    {
        #region Ctor

        public CreateOrderPayment(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long OrderId { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        public string? TransactionId { get; set; } = null!;

        public string? GatewayInitializationResponse { get; set; }

        #endregion
    }
}