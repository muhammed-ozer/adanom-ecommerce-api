namespace Adanom.Ecommerce.API.Data.Models
{
    public abstract class IBaseEntity<T>
    {
        public required T Id { get; set; }
    }
}
