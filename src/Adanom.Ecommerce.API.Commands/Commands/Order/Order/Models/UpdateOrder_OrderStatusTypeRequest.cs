namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateOrder_OrderStatusTypeRequest
    {
        public long Id { get; set; }

        public OrderStatusType NewOrderStatusType { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        public string? ShippingTrackingCode { get; set; }

        public DateTime? DeliveredAtUtc { get; set; }
    }
}