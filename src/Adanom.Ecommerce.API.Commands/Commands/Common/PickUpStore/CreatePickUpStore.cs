using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreatePickUpStore : IRequest<PickUpStoreResponse?>
    {
        #region Ctor

        public CreatePickUpStore(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public string DisplayName { get; set; } = null!;

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        #endregion
    }
}