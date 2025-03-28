using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteShippingAddress : IRequest<bool>
    {
        #region Ctor

        public DeleteShippingAddress(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        #endregion
    }
}