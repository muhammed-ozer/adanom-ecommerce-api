namespace Adanom.Ecommerce.API.Commands
{
    public class GetRelatedProducts : IRequest<IEnumerable<ProductResponse>>
    {
        #region Ctor

        public GetRelatedProducts(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}