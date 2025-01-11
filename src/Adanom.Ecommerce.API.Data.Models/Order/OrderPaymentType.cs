using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Data.Models
{
    public enum OrderPaymentType : byte
    {
        [EnumDisplayName("Kredi Kartı")]
        ONLINE_CREDIT_CARD,

        [EnumDisplayName("Havale/EFT")]
        BANK_TRANSFER,

        [EnumDisplayName("Teslimatta Kredi Kartı")]
        CREDIT_CARD_ON_DELIVERY,

        [EnumDisplayName("Teslimatta Nakit")]
        CASH_ON_DELIVERY
    }
}
