namespace Adanom.Ecommerce.API.Commands
{
    public interface ICacheInvalidator
    {
        string[] CacheKeys { get; }

        string Region { get; }

        bool InvalidateRegion { get; }

        string? Pattern { get; }
    }
}