namespace Adanom.Ecommerce.API.Logging
{
    public class BaseLogRequest
    {
        public LogLevel LogLevel { get; set; }

        public string Description { get; set; } = null!;
    }
}
