namespace Adanom.Ecommerce.API.Commands.Models
{
    public abstract class BaseResponseEntity<T>
    {
        public T Id { get; set; } = default!;
    }
}
