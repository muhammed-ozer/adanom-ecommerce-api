namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductPriceByProductId : IRequest<ProductPriceResponse?>
    {
        #region Ctor

        public GetProductPriceByProductId(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}