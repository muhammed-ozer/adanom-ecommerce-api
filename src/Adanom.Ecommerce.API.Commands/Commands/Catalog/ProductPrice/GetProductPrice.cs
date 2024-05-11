namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductPrice : IRequest<ProductPriceResponse?>
    {
        #region Ctor

        public GetProductPrice(long productSKUId)
        {
            ProductSKUId = productSKUId;
        }

        #endregion

        #region Properties

        public long ProductSKUId { get; set; }

        #endregion
    }
}