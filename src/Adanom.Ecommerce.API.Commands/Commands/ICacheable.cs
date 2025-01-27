namespace Adanom.Ecommerce.API.Commands
{
    public interface ICacheable
    {
        string CacheKey { get; }

        string Region { get; }

        TimeSpan? SlidingExpiration { get; }

        TimeSpan? AbsoluteExpiration { get; }
    }
}
