using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    [Transactional]
    public class UpdateOrderPayment : IRequest<bool>
    {
        #region Ctor

        public UpdateOrderPayment(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public long OrderId { get; set; }

        public string? GatewayResponse { get; set; }

        public bool Paid { get; set; }

        public DateTime? PaidAtUtc { get; set; }

        #endregion
    }
}