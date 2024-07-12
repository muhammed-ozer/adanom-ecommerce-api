using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateOrderShippingAddress : IRequest<OrderShippingAddressResponse?>
    {
        #region Ctor

        public CreateOrderShippingAddress(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public string Title { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string AddressCityName { get; set; } = null!;

        public string AddressDistrictName { get; set; } = null!;

        public string? PostalCode { get; set; }

        public string PhoneNumber { get; set; } = null!;

        #endregion
    }
}