namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductSKU : IRequest<ProductSKUResponse?>, ICacheable
    {
        #region Ctor

        public GetProductSKU(long id)
        {
            Id = id;
            Code = null;
        }


        public GetProductSKU(string code)
        {
            Id = 0;
            Code = code;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public string? Code { get; set; }

        #endregion

        #region ICacheable Properties

        public string CacheKey => !string.IsNullOrEmpty(Code) ? CacheKeyConstants.ProductSKU.CacheKeyByCode(Code) : CacheKeyConstants.ProductSKU.CacheKeyById(Id);

        public string Region => CacheKeyConstants.ProductSKU.Region;

        public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(30);

        public TimeSpan? AbsoluteExpiration => TimeSpan.FromMinutes(120);

        #endregion
    }
}