namespace Adanom.Ecommerce.API.Commands
{
    public class GetProduct_ProductCategories : IRequest<IEnumerable<ProductCategoryResponse>>
    {
        #region Ctor

        public GetProduct_ProductCategories(long productId)
        {
            ProductId = productId;
        }

        #endregion

        #region Properties

        public long ProductId { get; set; }

        #endregion
    }
}