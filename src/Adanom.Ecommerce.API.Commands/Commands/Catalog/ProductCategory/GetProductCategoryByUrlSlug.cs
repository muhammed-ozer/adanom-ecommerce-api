namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductCategoryByUrlSlug : IRequest<ProductCategoryResponse?>
    {
        #region Ctor

        public GetProductCategoryByUrlSlug(string urlSlug)
        {
            UrlSlug = urlSlug;
        }

        #endregion

        #region Properties

        public string UrlSlug { get; } = null!;

        #endregion
    }
}