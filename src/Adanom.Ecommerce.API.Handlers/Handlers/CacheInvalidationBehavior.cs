using Adanom.Ecommerce.API.Caching;

namespace Adanom.Ecommerce.API.Handlers
{
    public class CacheInvalidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IMemoryCacheManager _cacheManager;

        public CacheInvalidationBehavior(IMemoryCacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            if (request is not ICacheInvalidator invalidator)
            {
                return response;
            }

            if (invalidator.InvalidateRegion)
            {
                _cacheManager.RemoveByRegion(invalidator.Region);
            }

            if (!string.IsNullOrEmpty(invalidator.Pattern))
            {
                _cacheManager.RemoveByPattern(invalidator.Pattern);
            }

            if (invalidator.CacheKeys.Length > 0)
            {
                foreach (var key in invalidator.CacheKeys)
                {
                    _cacheManager.Remove(key);
                }
            }

            return response;
        }
    }
}