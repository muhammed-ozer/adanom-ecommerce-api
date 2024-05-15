namespace Adanom.Ecommerce.API.Commands
{
    public class GetAddressDistricts : IRequest<IEnumerable<AddressDistrictResponse>>
    {
        #region Ctor

        public GetAddressDistricts(long? addressCityId = null)
        {
            AddressCityId = addressCityId;
        }

        #endregion


        #region Properties

        public long? AddressCityId { get; set; }

        #endregion    
    }
}
