namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrderItems : IRequest<IEnumerable<OrderItemResponse>>
    {
        #region Ctor

        public GetOrderItems(GetOrderItemsFilter filter)
        {
            Filter = filter;
        }

        #endregion

        #region Properties

        public GetOrderItemsFilter Filter { get; set; }

        #endregion
    }
}
