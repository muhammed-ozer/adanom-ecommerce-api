namespace Adanom.Ecommerce.API.Logging
{
    public class BaseLogRequest
    {
        public LogLevel LogLevel { get; set; } = LogLevel.INFORMATION;

        public string Description { get; set; } = null!;

        public string? Exception { get; set; }
    }
}
