using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateUserRoles : IRequest<bool>
    {
        #region Ctor

        public UpdateUserRoles(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            Roles = new List<string>();
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public Guid Id { get; set; }

        public IEnumerable<string> Roles { get; set; }

        #endregion
    }
}