namespace Adanom.Ecommerce.API.Commands
{
    public class DoesProduct_ProductCategoryExists : IRequest<bool>
    {
        #region Ctor

        public DoesProduct_ProductCategoryExists(long productId, long productCategoryId)
        {
            ProductId = productId;
            ProductCategoryId = productCategoryId;
        }

        #endregion

        #region Properties

        public long ProductId { get; set; }

        public long ProductCategoryId { get; set; }

        #endregion
    }
}