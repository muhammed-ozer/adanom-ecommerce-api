using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdatePickUpStore : IRequest<bool>
    {
        #region Ctor

        public UpdatePickUpStore(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public string DisplayName { get; set; } = null!;

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        #endregion
    }
}