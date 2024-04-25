namespace Adanom.Ecommerce.API.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumLabelPropertiesAttribute : Attribute
    {
        public EnumLabelPropertiesAttribute(string? backgroundColor, string? foregroundColor)
        {
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
        }

        public string? BackgroundColor { get; }

        public string? ForegroundColor { get; }
    }
}
