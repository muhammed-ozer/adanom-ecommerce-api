namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductCategoryById : IRequest<ProductCategoryResponse?>
    {
        #region Ctor

        public GetProductCategoryById(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}