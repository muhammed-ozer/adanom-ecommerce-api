namespace Adanom.Ecommerce.API.Data.Models
{
    public abstract class BaseEntity<T>
    {
        public required T Id { get; set; }
    }
}
