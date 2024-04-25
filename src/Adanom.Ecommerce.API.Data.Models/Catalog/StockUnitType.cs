using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Data.Models
{
    public enum StockUnitType : byte
    {
        [EnumDisplayName("Adet")]
        PIECE,

        [EnumDisplayName("Paket")]
        PACKAGE,

        [EnumDisplayName("Takım")]
        SET
    }
}
