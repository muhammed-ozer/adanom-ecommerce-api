namespace Adanom.Ecommerce.API.Commands
{
    public class GetProduct : IRequest<ProductResponse?>, ICacheable
    {
        #region Ctor

        public GetProduct(long id)
        {
            Id = id;
            UrlSlug = null;
        }

        public GetProduct(string urlSlug)
        {
            Id = 0;
            UrlSlug = urlSlug;
        }

        #endregion

        #region Properties

        public long Id { get; }

        public string? UrlSlug { get; }

        #endregion

        #region ICacheable Properties

        public string CacheKey => !string.IsNullOrEmpty(UrlSlug) ? CacheKeyConstants.Product.CacheKeyByUrlSlug(UrlSlug) : CacheKeyConstants.Product.CacheKeyById(Id);

        public string Region => CacheKeyConstants.Product.Region;

        public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(30);

        public TimeSpan? AbsoluteExpiration => TimeSpan.FromMinutes(120);

        #endregion
    }
}