using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateLocalDeliveryProvider_AddressDistrict : IRequest<bool>
    {
        #region Ctor

        public CreateLocalDeliveryProvider_AddressDistrict(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long LocalDeliveryProviderId { get; set; }

        public long AddressDistrictId { get; set; }

        #endregion
    }
}