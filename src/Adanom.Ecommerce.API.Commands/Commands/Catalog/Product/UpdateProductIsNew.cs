using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateProductIsNew : IRequest<bool>
    {
        #region Ctor

        public UpdateProductIsNew(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public bool IsNew { get; set; }

        #endregion
    }
}