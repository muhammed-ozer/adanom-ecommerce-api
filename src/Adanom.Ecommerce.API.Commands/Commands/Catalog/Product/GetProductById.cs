namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductById : IRequest<ProductResponse?>
    {
        #region Ctor

        public GetProductById(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}