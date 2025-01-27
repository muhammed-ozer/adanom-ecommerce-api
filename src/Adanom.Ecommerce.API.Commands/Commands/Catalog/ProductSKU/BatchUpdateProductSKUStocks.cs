using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class BatchUpdateProductSKUStocks : IRequest<bool>, ICacheInvalidator
    {
        #region Fields

        private readonly List<string> _cacheKeys = new List<string>();

        #endregion

        #region Ctor

        public BatchUpdateProductSKUStocks(ClaimsPrincipal identity)
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

        public string Region => CacheKeyConstants.ProductSKU.Region;

        public bool InvalidateRegion => false;

        public string? Pattern => $"{CacheKeyConstants.ProductSKU.ByProductIdPattern}";

        #endregion

        public void AddCacheKey(string cacheKey)
        {
            _cacheKeys.Add(cacheKey);
        }
    }
}