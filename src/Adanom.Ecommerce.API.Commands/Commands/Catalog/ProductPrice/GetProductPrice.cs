namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductPrice : IRequest<ProductPriceResponse?>
    {
        #region Ctor

        public GetProductPrice(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}