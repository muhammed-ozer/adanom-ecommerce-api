namespace Adanom.Ecommerce.API.Handlers
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IMemoryCacheManager _memoryCacheManager;

        public CachingBehavior(IMemoryCacheManager memoryCacheManager)
        {
            _memoryCacheManager = memoryCacheManager;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not ICacheable cacheable)
            {
                return await next();
            }

            if (_memoryCacheManager.TryGet<TResponse>(cacheable.CacheKey, out var cachedResponse))
            {
                return cachedResponse;
            }

            var response = await next();

            if (response == null)
            {
                return response;
            }

            var options = new CacheOptions
            {
                CacheKey = cacheable.CacheKey,
                Region = cacheable.Region,
                SlidingExpiration = cacheable.SlidingExpiration,
                AbsoluteExpiration = cacheable.AbsoluteExpiration
            };

            _memoryCacheManager.Set(cacheable.CacheKey, response, options);

            return response;
        }
    }
}