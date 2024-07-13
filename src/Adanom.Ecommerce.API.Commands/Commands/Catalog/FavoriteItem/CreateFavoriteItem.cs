using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateFavoriteItem : IRequest<bool>
    {
        #region Ctor

        public CreateFavoriteItem(ClaimsPrincipal identity)
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