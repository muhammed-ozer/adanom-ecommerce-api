namespace Adanom.Ecommerce.API.Commands
{
    public class GetLocalDeliveryProvider_SupportedAddressDistricts : IRequest<IEnumerable<AddressDistrictResponse>>
    {
        #region Ctor

        public GetLocalDeliveryProvider_SupportedAddressDistricts(long localDeliveryProviderId)
        {
            LocalDeliveryProviderId = localDeliveryProviderId;
        }

        #endregion

        #region Properties

        public long LocalDeliveryProviderId { get; set; }

        #endregion
    }
}