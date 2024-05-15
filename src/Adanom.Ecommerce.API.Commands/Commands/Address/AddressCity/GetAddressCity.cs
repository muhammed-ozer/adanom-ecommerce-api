namespace Adanom.Ecommerce.API.Commands
{
    public class GetAddressCity : IRequest<AddressCityResponse?>
    {
        #region Ctor

        public GetAddressCity(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}