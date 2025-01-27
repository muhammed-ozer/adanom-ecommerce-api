namespace Adanom.Ecommerce.API.Handlers
{
    public class CacheInvalidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        #region Fields

        private readonly IMemoryCacheManager _memoryCacheManager;

        #endregion

        #region Ctor

        public CacheInvalidationBehavior(IMemoryCacheManager memoryCacheManager)
        {
            _memoryCacheManager = memoryCacheManager;
        }

        #endregion

        #region CacheInvalidationBehavior Members

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            if (request is not ICacheInvalidator invalidator)
            {
                return response;
            }

            if (invalidator.InvalidateRegion)
            {
                _memoryCacheManager.RemoveByRegion(invalidator.Region);
            }

            if (!string.IsNullOrEmpty(invalidator.Pattern))
            {
                _memoryCacheManager.RemoveByPattern(invalidator.Pattern);
            }

            if (invalidator.CacheKeys.Length > 0)
            {
                foreach (var key in invalidator.CacheKeys)
                {
                    _memoryCacheManager.Remove(key);
                }
            }

            return response;
        }

        #endregion
    }
}