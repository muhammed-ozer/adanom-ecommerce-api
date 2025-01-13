namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateOrderRequest
    {
        public DeliveryType DeliveryType { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        public long? ShippingProviderId { get; set; }

        public long? PickUpStoreId { get; set; }

        public long? LocalDeliveryProviderId { get; set; }

        public long ShippingAddressId { get; set; }

        public long? BillingAddressId { get; set; }

        public string? Note { get; set; }

        public bool DistanceSellingContract { get; set; }

        public bool PreliminaryInformationForm { get; set; }
    }
}