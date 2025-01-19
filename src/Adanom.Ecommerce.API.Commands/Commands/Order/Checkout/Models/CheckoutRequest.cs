namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CheckoutRequest
    {
        public DeliveryType DeliveryType { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        public long ShippingAddressId { get; set; }

        public long? BillingAddressId { get; set; }

        public long? ShippingProviderId { get; set; }

        public long? PickUpStoreId { get; set; }

        public long? LocalDeliveryProviderId { get; set; }
    }
}