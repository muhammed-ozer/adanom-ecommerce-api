namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteStockReservations : IRequest<bool>, ICacheInvalidator
    {
        #region Fields

        private readonly List<string> _cacheKeys = new List<string>();

        #endregion

        #region Ctor

        public DeleteStockReservations(long orderId, bool? decreaseProductStockQuantity = null)
        {
            OrderId = orderId;
            DecreaseProductStockQuantity = decreaseProductStockQuantity;
        }

        #endregion

        #region Properties

        public long OrderId { get; set; }

        public bool? DecreaseProductStockQuantity { get; set; }

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
