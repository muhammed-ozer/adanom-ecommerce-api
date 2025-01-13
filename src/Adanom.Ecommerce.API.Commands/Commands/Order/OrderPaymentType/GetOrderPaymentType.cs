namespace Adanom.Ecommerce.API.Commands
{
    public class GetOrderPaymentType : IRequest<OrderPaymentTypeResponse>
    {
        #region Ctor

        public GetOrderPaymentType(OrderPaymentType orderPaymentType)
        {
            OrderPaymentType = orderPaymentType;
        }

        #endregion

        #region Properties

        public OrderPaymentType OrderPaymentType { get; set; }

        #endregion
    }
}
