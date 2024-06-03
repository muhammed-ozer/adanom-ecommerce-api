using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public sealed class MarkAsReadNotifications : IRequest<bool>
    {
        #region Ctor

        public MarkAsReadNotifications(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            Ids = new List<int>();
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public IEnumerable<int> Ids { get; set; }

        #endregion
    }
}
