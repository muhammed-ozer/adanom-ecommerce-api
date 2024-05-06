namespace Adanom.Ecommerce.API.Commands
{
    public class GetProductTag : IRequest<ProductTagResponse?>
    {
        #region Ctor

        public GetProductTag(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}