namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateShoppingCart_LastModifiedDate : IRequest<bool>
    {
        #region Ctor

        public UpdateShoppingCart_LastModifiedDate(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}