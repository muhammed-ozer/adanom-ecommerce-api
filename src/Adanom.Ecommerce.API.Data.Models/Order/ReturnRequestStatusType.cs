using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Data.Models
{
    public enum ReturnRequestStatusType : byte
    {
        [EnumDisplayName("İade talebi oluşturuldu")]
        RECEIVED,

        [EnumDisplayName("İşlemde")]
        IN_PROGRESS,

        [EnumDisplayName("Onaylandı")]
        APPROVED,

        [EnumDisplayName("İade ödemesi gerçekleştirildi")]
        REFUND_MADE,

        [EnumDisplayName("Reddedildi")]
        DISAPPROVED,

        [EnumDisplayName("İptal Edildi")]
        CANCEL
    }
}
