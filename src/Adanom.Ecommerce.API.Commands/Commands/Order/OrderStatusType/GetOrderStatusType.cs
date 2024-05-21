namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrderStatusType : IRequest<OrderStatusTypeResponse>
    {
        #region Ctor

        public GetOrderStatusType(OrderStatusType stockUnitType)
        {
            OrderStatusType = stockUnitType;
        }

        #endregion

        #region Properties

        public OrderStatusType OrderStatusType { get; set; }

        #endregion
    }
}
