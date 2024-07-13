using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateStockNotificationItem : IRequest<bool>
    {
        #region Ctor

        public CreateStockNotificationItem(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long ProductId { get; set; }

        #endregion
    }
}