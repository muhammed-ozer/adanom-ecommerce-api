using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateOrderBillingAddress : IRequest<OrderBillingAddressResponse?>
    {
        #region Ctor

        public CreateOrderBillingAddress(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public string Title { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string AddressCityName { get; set; } = null!;

        public string AddressDistrictName { get; set; } = null!;

        public string? PostalCode { get; set; }

        public string TaxAdministration { get; set; } = null!;

        public string TaxNumber { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        #endregion
    }
}