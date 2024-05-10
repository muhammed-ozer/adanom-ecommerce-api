namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductSKUById : IRequest<ProductSKUResponse?>
    {
        #region Ctor

        public GetProductSKUById(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}