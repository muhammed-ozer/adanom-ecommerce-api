using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class ProceedToCheckout : IRequest<bool>
    {
        #region Ctor

        public ProceedToCheckout(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        #endregion
    }
}