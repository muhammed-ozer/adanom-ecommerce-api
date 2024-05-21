namespace Adanom.Ecommerce.API.Commands.Models
{
    public class OrderResponse : BaseResponseEntity<long>
    {
        public OrderResponse()
        {
            Items = new List<OrderItemResponse>();
        }

        public Guid UserId { get; set; }

        public long ShippingAddressId { get; set; }

        public long? BillingAddressId { get; set; }

        public long? ShippingProviderId { get; set; }

        public long? PickUpStoreId { get; set; }

        public OrderStatusTypeResponse? OrderStatusType { get; set; }

        public DeliveryTypeResponse? DeliveryType { get; set; }

        public string OrderNumber { get; set; } = null!;

        public decimal SubTotal { get; set; }

        public decimal SubTotalDiscount { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal ShippingFeeSubTotal { get; set; }

        public decimal ShippingFeeTax { get; set; }

        public decimal GrandTotal { get; set; }

        public bool IsFreeShipping { get; set; }

        public string? Note { get; set; }

        public string? ShippingTrackingCode { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? DeliveredAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public UserResponse? User { get; set; }

        public ShippingProviderResponse? ShippingProvider { get; set; }

        public PickUpStoreResponse? PickUpStore { get; set; }

        public ICollection<OrderItemResponse> Items { get; set; }

        public OrderPaymentResponse? OrderPayment { get; set; }

        //public ShippingAddressResponse ShippingAddress { get; set; } = null!;

        //public BillingAddressResponse? BillingAddress { get; set; }
    }
}