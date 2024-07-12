namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrderShippingAddress : IRequest<OrderShippingAddressResponse?>
    {
        #region Ctor

        public GetOrderShippingAddress(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}