namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductPriceByProductId : IRequest<ProductPriceResponse?>
    {
        #region Ctor

        public GetProductPriceByProductId(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion

        #region ICacheable Properties

        public string CacheKey => CacheKeyConstants.ProductPrice.CacheKeyByProductId(Id);

        public string Region => CacheKeyConstants.ProductPrice.Region;

        public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(30);

        public TimeSpan? AbsoluteExpiration => TimeSpan.FromMinutes(120);

        #endregion
    }
}