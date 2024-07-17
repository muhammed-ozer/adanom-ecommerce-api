namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductSKUByProductId : IRequest<ProductSKUResponse?>
    {
        #region Ctor

        public GetProductSKUByProductId(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}