namespace Adanom.Ecommerce.API.Data.Models
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; } = default!;
    }
}
