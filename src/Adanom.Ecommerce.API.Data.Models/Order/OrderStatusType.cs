using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Data.Models
{
    public enum OrderStatusType : byte
    {
        [EnumDisplayName("Ödeme Bekleniyor")]
        PAYMENT_PENDING,

        [EnumDisplayName("Yeni Sipariş")]
        NEW,

        [EnumDisplayName("Hazırlanıyor")]
        IN_PROGRESS,

        [EnumDisplayName("Hazırlandı")]
        READY,

        [EnumDisplayName("Kargo Firmasına Teslim Edildi")]
        DELIVERED_TO_SHIPPING_PROVIDER,

        [EnumDisplayName("Teslim Edildi")]
        DELIVERED_TO_CUSTOMER,

        [EnumDisplayName("İptal")]
        CANCEL
    }
}
