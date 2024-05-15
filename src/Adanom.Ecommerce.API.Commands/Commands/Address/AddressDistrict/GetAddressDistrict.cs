namespace Adanom.Ecommerce.API.Commands
{
    public class GetAddressDistrict : IRequest<AddressDistrictResponse?>
    {
        #region Ctor

        public GetAddressDistrict(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}