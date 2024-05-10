namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductByUrlSlug : IRequest<ProductResponse?>
    {
        #region Ctor

        public GetProductByUrlSlug(string urlSlug)
        {
            UrlSlug = urlSlug;
        }

        #endregion

        #region Properties

        public string UrlSlug { get; set; }

        #endregion
    }
}