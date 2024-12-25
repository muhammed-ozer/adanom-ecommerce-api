using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetShippingAddressesCount : IRequest<long>
    {
        #region Ctor

        public GetShippingAddressesCount(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        #endregion
    }
}
