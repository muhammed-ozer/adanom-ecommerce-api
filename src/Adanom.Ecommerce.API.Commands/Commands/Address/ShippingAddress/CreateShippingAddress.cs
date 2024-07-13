using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateShippingAddress : IRequest<bool>
    {
        #region Ctor

        public CreateShippingAddress(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long AddressCityId { get; set; }

        public long AddressDistrictId { get; set; }

        public string Title { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? PostalCode { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public bool IsDefault { get; set; }

        #endregion
    }
}