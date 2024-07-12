namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrderBillingAddress : IRequest<OrderBillingAddressResponse?>
    {
        #region Ctor

        public GetOrderBillingAddress(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}