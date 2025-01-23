namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ReturnRequestResponse : BaseResponseEntity<long>
    {
        public ReturnRequestResponse()
        {
            Items = new List<ReturnRequestItemResponse>();
        }

        public long OrderId { get; set; }

        public long? ShippingProviderId { get; set; }

        public long? PickUpStoreId { get; set; }

        public long? LocalDeliveryProviderId { get; set; }

        public ReturnRequestStatusTypeResponse ReturnRequestStatusType { get; set; } = null!;

        public DeliveryTypeResponse DeliveryType { get; set; } = null!;

        public string ReturnRequestNumber { get; set; } = null!;

        public decimal GrandTotal { get; set; }

        public string? ShippingTrackingCode { get; set; }

        public string? DisapprovedReasonMessage { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public OrderResponse? Order { get; set; }

        public ShippingProviderResponse? ShippingProvider { get; set; }

        public PickUpStoreResponse? PickUpStore { get; set; }

        public LocalDeliveryProviderResponse? LocalDeliveryProvider { get; set; }

        public ICollection<ReturnRequestItemResponse> Items { get; set; }
    }
}