namespace Adanom.Ecommerce.API.Commands
{
    public class GetProduct : IRequest<ProductResponse?>
    {
        #region Ctor

        public GetProduct(long id)
        {
            Id = id;
        }

        public GetProduct(string urlSlug)
        {
            UrlSlug = urlSlug;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public string UrlSlug { get; set; } = null!;

        #endregion
    }
}