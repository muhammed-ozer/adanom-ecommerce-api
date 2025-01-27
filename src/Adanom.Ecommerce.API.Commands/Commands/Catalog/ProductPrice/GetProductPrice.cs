namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductPrice : IRequest<ProductPriceResponse?>, ICacheable
    {
        #region Ctor

        public GetProductPrice(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion

        #region ICacheable Properties

        public string CacheKey => CacheKeyConstants.ProductPrice.CacheKeyById(Id);

        public string Region => CacheKeyConstants.ProductPrice.Region;

        public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(30);

        public TimeSpan? AbsoluteExpiration => TimeSpan.FromMinutes(120);

        #endregion
    }
}