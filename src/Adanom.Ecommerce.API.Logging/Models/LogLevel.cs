using Adanom.Ecommerce.API.Data.Attributes;

namespace Adanom.Ecommerce.API.Logging
{
    public enum LogLevel : byte
    {
        [EnumLabelProperties("#3399ff", "#ffffff")]
        INFORMATION,

        [EnumLabelProperties("#ffa64d", "#ffffff")]
        WARNING,

        [EnumLabelProperties("#ff6600", "#ffffff")]
        ERROR,

        [EnumLabelProperties("ff0000", "#ffffff")]
        CRITICAL
    }
}
