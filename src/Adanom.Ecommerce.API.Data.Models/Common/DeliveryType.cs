using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Data.Models
{
    public enum DeliveryType : byte
    {
        [EnumDisplayName("Kargo ile teslim")]
        CARGO_SHIPMENT,

        [EnumDisplayName("Mağazadan teslim")]
        PICK_UP_FROM_STORE,

        [EnumDisplayName("Yerel teslimat")]
        LOCAL_DELIVERY,
    }
}
