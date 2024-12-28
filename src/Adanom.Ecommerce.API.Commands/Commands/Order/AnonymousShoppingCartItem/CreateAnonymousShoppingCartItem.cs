namespace Adanom.Ecommerce.API.Commands
{
    public class CreateAnonymousShoppingCartItem : IRequest<CreateAnonymousShoppingCartItemResponse>
    {
        #region Ctor

        public CreateAnonymousShoppingCartItem()
        {
        }

        #endregion

        #region Properties

        public Guid? AnonymousShoppingCartId { get; set; }

        public long ProductId { get; set; }

        public int Amount { get; set; }

        #endregion
    }
}