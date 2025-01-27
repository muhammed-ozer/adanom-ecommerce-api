using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateProductName : IRequest<ProductResponse?>, ICacheInvalidator
    {
        #region Fields

        private readonly List<string> _cacheKeys = new List<string>();

        #endregion

        #region Ctor

        public UpdateProductName(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public string Name { get; set; } = null!;

        #endregion

        #region ICacheInvalidator Properties

        public string[] CacheKeys => _cacheKeys.ToArray();

        public string Region => CacheKeyConstants.Product.Region;

        public bool InvalidateRegion => false;

        public string? Pattern => null;

        #endregion

        public void AddCacheKey(string cacheKey)
        {
            _cacheKeys.Add(cacheKey);
        }
    }
}