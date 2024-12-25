using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetBillingAddressesCount : IRequest<long>
    {
        #region Ctor

        public GetBillingAddressesCount(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        #endregion
    }
}
