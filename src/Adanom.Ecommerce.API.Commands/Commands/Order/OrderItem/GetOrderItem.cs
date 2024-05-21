namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrderItem : IRequest<OrderItemResponse?>
    {
        #region Ctor

        public GetOrderItem(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}
