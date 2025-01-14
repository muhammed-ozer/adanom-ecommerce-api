using Adanom.Ecommerce.API.Logging;

namespace Adanom.Ecommerce.API.Data.Models
{
    public abstract class BaseLogEntity<T>
    {
        public T Id { get; set; } = default!;

        public string? Exception { get; set; }

        public LogLevel LogLevel { get; set; }
    }
}
