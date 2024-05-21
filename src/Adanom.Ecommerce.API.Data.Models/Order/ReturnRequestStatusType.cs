using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Data.Models
{
    public enum ReturnRequestStatusType : byte
    {
        [EnumDisplayName("İade talebi oluşturuldu")]
        RECEIVED,

        [EnumDisplayName("Paket bekleniyor")]
        PACKAGE_PENDING,

        [EnumDisplayName("İşlemde")]
        IN_PROGRESS,

        [EnumDisplayName("Onaylandı")]
        APPROVED,

        [EnumDisplayName("İade ödemesi gerçekleştirildi")]
        REFUND_MADE,

        [EnumDisplayName("Onaylanmadı")]
        DISAPPROVED,

        [EnumDisplayName("İptal")]
        CANCEL
    }
}
