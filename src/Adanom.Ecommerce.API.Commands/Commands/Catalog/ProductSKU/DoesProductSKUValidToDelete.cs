namespace Adanom.Ecommerce.API.Commands
{
    public class DoesProductSKUValidToDelete : IRequest<bool>
    {
        #region Ctor

        public DoesProductSKUValidToDelete(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}