using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Data.Models
{
    public enum OrderPaymentType : byte
    {
        [EnumDisplayName("Online Ödeme")]
        ONLINE_PAYMENT,

        [EnumDisplayName("Havale/EFT")]
        [EnumDiscountRate(5)]
        BANK_TRANSFER,

        [EnumDisplayName("Teslimatta Kredi Kartı")]
        CREDIT_CARD_ON_DELIVERY,

        [EnumDisplayName("Teslimatta Nakit")]
        [EnumDiscountRate(5)]
        CASH_ON_DELIVERY
    }
}
