using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class BatchUpdateProductPrices : IRequest<bool>, ICacheInvalidator
    {
        #region Fields

        private readonly List<string> _cacheKeys = new List<string>();

        #endregion

        #region Ctor

        public BatchUpdateProductPrices(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public UploadedFile File { get; set; } = null!;

        #endregion

        #region ICacheInvalidator Properties

        public string[] CacheKeys => _cacheKeys.ToArray();

        public string Region => CacheKeyConstants.ProductPrice.Region;

        public bool InvalidateRegion => false;

        public string? Pattern => $"{CacheKeyConstants.ProductPrice.ByProductIdPattern}";

        #endregion

        public void AddCacheKey(string cacheKey)
        {
            _cacheKeys.Add(cacheKey);
        }
    }
}