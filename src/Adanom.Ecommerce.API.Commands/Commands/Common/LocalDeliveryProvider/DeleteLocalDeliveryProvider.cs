using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteLocalDeliveryProvider : IRequest<bool>
    {
        #region Ctor

        public DeleteLocalDeliveryProvider(ClaimsPrincipal identity)
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