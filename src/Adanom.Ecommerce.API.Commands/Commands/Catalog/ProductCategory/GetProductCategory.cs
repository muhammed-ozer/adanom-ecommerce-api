namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductCategory : IRequest<ProductCategoryResponse?>
    {
        #region Ctor

        public GetProductCategory(long id)
        {
            Id = id;
        }

        public GetProductCategory(string urlSlug)
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