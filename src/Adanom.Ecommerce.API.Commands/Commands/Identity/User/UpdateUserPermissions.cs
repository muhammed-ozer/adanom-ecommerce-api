using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateUserPermissions : IRequest<bool>
    {
        #region Ctor

        public UpdateUserPermissions(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public bool AllowCommercialEmails { get; set; }

        public bool AllowCommercialSMS { get; set; }

        #endregion
    }
}