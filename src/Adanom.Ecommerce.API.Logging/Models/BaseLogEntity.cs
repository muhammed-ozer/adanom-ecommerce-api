namespace Adanom.Ecommerce.API.Data.Models
{
    public abstract class BaseLogEntity<T>
    {
        public T Id { get; set; } = default!;

        public string? Exception { get; set; }
    }
}
