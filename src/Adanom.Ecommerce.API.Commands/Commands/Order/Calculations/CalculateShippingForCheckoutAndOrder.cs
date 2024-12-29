namespace Adanom.Ecommerce.API.Commands
{
    public class CalculateShippingForCheckoutAndOrder : IRequest<CalculateShippingForCheckoutAndOrderResponse?>
    {
        #region Ctor

        public CalculateShippingForCheckoutAndOrder(DeliveryType deliveryType, decimal grandTotal, long? shippingProviderId)
        {
            DeliveryType = deliveryType;
            GrandTotal = grandTotal;
            ShippingProviderId = shippingProviderId;
        }

        #endregion

        #region Properties

        public DeliveryType DeliveryType { get; }

        public decimal GrandTotal { get; }

        public long? ShippingProviderId { get; }

        #endregion
    }
}