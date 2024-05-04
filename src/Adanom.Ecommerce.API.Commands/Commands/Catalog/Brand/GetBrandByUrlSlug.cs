namespace Adanom.Ecommerce.API.Commands
{
    public class GetBrandByUrlSlug : IRequest<BrandResponse?>
    {
        #region Ctor

        public GetBrandByUrlSlug(string urlSlug)
        {
            UrlSlug = urlSlug;
        }

        #endregion

        #region Properties

        public string UrlSlug { get; } = null!;

        #endregion
    }
}