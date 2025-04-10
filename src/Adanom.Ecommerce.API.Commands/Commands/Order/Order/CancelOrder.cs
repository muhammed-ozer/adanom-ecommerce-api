using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    [Transactional]
    public class CancelOrder : IRequest<bool>
    {
        #region Ctor

        public CancelOrder(ClaimsPrincipal identity)
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
