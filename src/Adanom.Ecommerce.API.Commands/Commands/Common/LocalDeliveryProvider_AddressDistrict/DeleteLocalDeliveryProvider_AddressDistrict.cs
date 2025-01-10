using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteLocalDeliveryProvider_AddressDistrict : IRequest<bool>
    {
        #region Ctor

        public DeleteLocalDeliveryProvider_AddressDistrict(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long LocalDeliveryProviderId { get; set; }

        public long? AddressDistrictId { get; set; }

        #endregion
    }
}