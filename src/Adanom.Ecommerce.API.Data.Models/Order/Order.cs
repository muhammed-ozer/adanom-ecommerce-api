using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(OrderNumber), IsUnique = true)]
    public class Order : BaseEntity<long>
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public Guid UserId { get; set; }

        public long OrderShippingAddressId { get; set; }

        public long? OrderBillingAddressId { get; set; }

        public long? ShippingProviderId { get; set; }

        public long? PickUpStoreId { get; set; }

        public long? LocalDeliveryProviderId { get; set; }

        public OrderStatusType OrderStatusType { get; set; }

        public DeliveryType DeliveryType { get; set; }

        [StringLength(25)]
        public string OrderNumber { get; set; } = null!;

        public decimal SubTotal { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal? UserDefaultDiscountRateBasedDiscount { get; set; }

        public decimal? DiscountByOrderPaymentType { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal ShippingFeeTotal { get; set; }

        public decimal ShippingFeeTax { get; set; }

        public decimal GrandTotal { get; set; }

        public bool IsFreeShipping { get; set; }

        [StringLength(100)]
        public string? Note { get; set; }

        [StringLength(250)]
        public string? ShippingTrackingCode { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? DeliveredAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public User User { get; set; } = null!;

        public ShippingProvider? ShippingProvider { get; set; }

        public PickUpStore? PickUpStore { get; set; }

        public LocalDeliveryProvider? LocalDeliveryProvider { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public OrderShippingAddress OrderShippingAddress { get; set; } = null!;

        public OrderBillingAddress? OrderBillingAddress { get; set; }
    }
}
