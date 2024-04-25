namespace Adanom.Ecommerce.API.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumDisplayNameAttribute : Attribute
    {
        public EnumDisplayNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
