using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateCompany : IRequest<bool>
    {
        #region Ctor

        public UpdateCompany(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long AddressCityId { get; set; }

        public long AddressDistrictId { get; set; }

        public long TaxAdministrationId { get; set; }

        public string LegalName { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string TaxNumber { get; set; } = null!;

        public string MersisNumber { get; set; } = null!;

        #endregion
    }
}