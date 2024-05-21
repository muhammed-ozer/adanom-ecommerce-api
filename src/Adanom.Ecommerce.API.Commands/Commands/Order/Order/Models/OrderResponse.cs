using System.ComponentModel.DataAnnotations;

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

        [StringLength(25)]
        public string OrderNumber { get; set; } = null!;

        public decimal SubTotal { get; set; }

        public decimal SubTotalDiscount { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal ShippingFeeSubTotal { get; set; }

        public decimal ShippingFeeTax { get; set; }

        public decimal GrandTotal { get; set; }

        public bool IsFreeShipping { get; set; }

        public string? Note { get; set; }

        public string? ShippingTransactionCode { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? DeliveredAtUtc { get; set; }

        public UserResponse? User { get; set; }

        public ShippingProviderResponse? ShippingProvider { get; set; }

        public PickUpStoreResponse? PickUpStore { get; set; }

        public ICollection<OrderItemResponse> Items { get; set; }

        //public ShippingAddressResponse ShippingAddress { get; set; } = null!;

        //public BillingAddressResponse? BillingAddress { get; set; }

        //public OrderPaymentResponse OrderPayment { get; set; } = null!;
    }
}