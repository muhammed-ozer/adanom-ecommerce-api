namespace Adanom.Ecommerce.API.Commands
{
    public class GetAllowedPaymentTypesByDeliveryType : IRequest<IEnumerable<OrderPaymentTypeResponse>>
    {
        #region Ctor

        public GetAllowedPaymentTypesByDeliveryType(DeliveryType deliveryType)
        {
            DeliveryType = deliveryType;
        }

        #endregion

        #region Properties

        public DeliveryType DeliveryType { get; set; }

        #endregion
    }
}
