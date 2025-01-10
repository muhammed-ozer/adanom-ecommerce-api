namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CheckoutRequest
    {
        public DeliveryType DeliveryType { get; set; }

        public long? ShippingProviderId { get; set; }

        public long? PickUpStoreId { get; set; }

        public long? LocalDeliveryProviderId { get; set; }
    }
}