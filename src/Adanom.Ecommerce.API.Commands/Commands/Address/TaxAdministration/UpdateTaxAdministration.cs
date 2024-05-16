using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateTaxAdministration : IRequest<bool>
    {
        #region Ctor

        public UpdateTaxAdministration(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public string Name { get; set; } = null!;

        #endregion
    }
}