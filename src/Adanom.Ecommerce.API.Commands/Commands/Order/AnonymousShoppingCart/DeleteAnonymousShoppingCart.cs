namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteAnonymousShoppingCart : IRequest<bool>
    {
        #region Ctor

        public DeleteAnonymousShoppingCart(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public Guid Id { get; }

        #endregion
    }
}