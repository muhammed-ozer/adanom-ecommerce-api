namespace Adanom.Ecommerce.API.Caching
{
    public class CacheOptions
    {
        public TimeSpan? SlidingExpiration { get; set; }

        public TimeSpan? AbsoluteExpiration { get; set; }

        public string? Region { get; set; }

        public string? CacheKey { get; set; }
    }
}