namespace Adanom.Ecommerce.API.Commands
{
    public class GetAnonymousShoppingCartItem : IRequest<AnonymousShoppingCartItemResponse?>
    {
        #region Ctor

        public GetAnonymousShoppingCartItem(long id)
        {
            Id = id;
        }

        public GetAnonymousShoppingCartItem(Guid anonymousShoppingCartId, long productId)
        {
            AnonymousShoppingCartId = anonymousShoppingCartId;
            ProductId = productId;
        }

        #endregion

        #region Properties

        public long? Id { get; set; }

        public Guid? AnonymousShoppingCartId { get; set; }

        public long? ProductId { get; set; }

        #endregion
    }
}
