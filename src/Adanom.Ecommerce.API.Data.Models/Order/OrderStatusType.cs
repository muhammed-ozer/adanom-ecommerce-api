using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Data.Models
{
    public enum OrderStatusType : byte
    {
        [EnumDisplayName("Ödeme Bekleniyor")]
        PAYMENT_PENDING,

        [EnumDisplayName("Yeni Sipariş")]
        NEW,

        [EnumDisplayName("Onaylandı")]
        APPROVED,

        [EnumDisplayName("İşlemde")]
        IN_PROGRESS,

        [EnumDisplayName("Hazırlandı")]
        READY,

        [EnumDisplayName("Teslimatta")]
        ON_DELIVERY,

        [EnumDisplayName("Tamamlandı")]
        DONE,

        [EnumDisplayName("İptal Edildi")]
        CANCEL
    }
}
