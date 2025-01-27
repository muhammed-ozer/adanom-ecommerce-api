namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductSKUByProductId : IRequest<ProductSKUResponse?>, ICacheable
    {
        #region Ctor

        public GetProductSKUByProductId(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion

        #region ICacheable Properties

        public string CacheKey => CacheKeyConstants.ProductSKU.CacheKeyByProductId(Id);

        public string Region => CacheKeyConstants.ProductSKU.Region;

        public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(30);

        public TimeSpan? AbsoluteExpiration => TimeSpan.FromMinutes(120);

        #endregion
    }
}