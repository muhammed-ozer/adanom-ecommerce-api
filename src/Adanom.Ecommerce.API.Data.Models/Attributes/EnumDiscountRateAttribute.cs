namespace Adanom.Ecommerce.API.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumDiscountRateAttribute : Attribute
    {
        public EnumDiscountRateAttribute(byte discounRate)
        {
            DiscountRate = discounRate;
        }

        public byte DiscountRate { get; }
    }
}
