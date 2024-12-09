using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Data.Models
{
    public enum NotificationType : byte
    {
        [EnumDisplayName("Yeni Kullanıcı")]
        [EnumLabelProperties("#ffee93", "#ffffff")]
        NEW_USER,

        [EnumDisplayName("Yeni İade Talebi")]
        [EnumLabelProperties("#ffee93", "#ffffff")]
        NEW_RETURN_REQUEST,

        [EnumDisplayName("Yeni Sipariş")]
        [EnumLabelProperties("#ffee93", "#ffffff")]
        NEW_ORDER
    }
}
