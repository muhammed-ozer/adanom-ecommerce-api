namespace Adanom.Ecommerce.API.Data.Models
{
    public enum OrderStatusType : byte
    {
        RECEIVED,
        IN_PROGRESS,
        READY,
        PICKED_UP_BY_SHIPPING_PROVIDER,
        DELIVERED,
        CANCEL
    }
}
