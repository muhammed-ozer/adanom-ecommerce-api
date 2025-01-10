namespace Adanom.Ecommerce.API.Commands
{
    public class CalculateShippingForCheckoutAndOrder : IRequest<CalculateShippingForCheckoutAndOrderResponse?>
    {
        #region Ctor

        public CalculateShippingForCheckoutAndOrder(DeliveryType deliveryType, decimal grandTotal, long? shippingProviderId, long? localDeliveryProviderId)
        {
            DeliveryType = deliveryType;
            GrandTotal = grandTotal;
            ShippingProviderId = shippingProviderId;
            LocalDeliveryProviderId = localDeliveryProviderId;

        }

        #endregion

        #region Properties

        public DeliveryType DeliveryType { get; }

        public decimal GrandTotal { get; }

        public long? ShippingProviderId { get; }

        public long? LocalDeliveryProviderId { get; }

        #endregion
    }
}