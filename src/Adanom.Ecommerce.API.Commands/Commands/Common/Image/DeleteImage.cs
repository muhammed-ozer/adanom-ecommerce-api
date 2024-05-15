using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteImage : IRequest<bool>
    {
        #region Ctor

        public DeleteImage(ClaimsPrincipal identity)
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