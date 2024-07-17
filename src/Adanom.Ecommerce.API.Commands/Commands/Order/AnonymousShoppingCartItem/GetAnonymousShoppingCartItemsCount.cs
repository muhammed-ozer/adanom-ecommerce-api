namespace Adanom.Ecommerce.API.Commands
{
    public class GetAnonymousShoppingCartItemsCount : IRequest<int>
    {
        #region Ctor

        public GetAnonymousShoppingCartItemsCount(Guid anonymousSHoppingCartId)
        {
            AnonymousSHoppingCartId = anonymousSHoppingCartId;
        }

        #endregion

        #region Properties

        public Guid AnonymousSHoppingCartId { get; }

        #endregion
    }
}
