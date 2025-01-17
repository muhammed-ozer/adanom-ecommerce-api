namespace Adanom.Ecommerce.API.Commands
{
    public class GetCreatedOrderStatusTypeByOrderPaymentType : IRequest<OrderStatusTypeResponse>
    {
        #region Ctor

        public GetCreatedOrderStatusTypeByOrderPaymentType(OrderPaymentType orderPaymentType)
        {
            OrderPaymentType = orderPaymentType;
        }

        #endregion

        #region Properties

        public OrderPaymentType OrderPaymentType { get; set; }

        #endregion
    }
}
