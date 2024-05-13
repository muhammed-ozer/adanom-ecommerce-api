namespace Adanom.Ecommerce.API.Commands
{
    public class GetBrand : IRequest<BrandResponse?>
    {
        #region Ctor

        public GetBrand(long id)
        {
            Id = id;
        }
        public GetBrand(string urlSlug)
        {
            UrlSlug = urlSlug;
        }

        #endregion

        #region Properties

        public long Id { get; }

        public string UrlSlug { get; } = null!;

        #endregion
    }
}