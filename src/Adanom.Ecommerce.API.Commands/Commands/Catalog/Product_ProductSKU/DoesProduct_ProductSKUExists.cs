namespace Adanom.Ecommerce.API.Commands
{
    public class DoesProduct_ProductSKUExists : IRequest<bool>
    {
        #region Ctor

        public DoesProduct_ProductSKUExists(long productId, long productSKUId)
        {
            ProductId = productId;
            ProductSKUId = productSKUId;
        }

        #endregion

        #region Properties

        public long ProductId { get; set; }

        public long ProductSKUId { get; set; }

        #endregion
    }
}