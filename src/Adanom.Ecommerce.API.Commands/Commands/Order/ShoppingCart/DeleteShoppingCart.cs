namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteShoppingCart : IRequest<bool>
    {
        #region Ctor

        public DeleteShoppingCart(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; }

        #endregion
    }
}