namespace Adanom.Ecommerce.API.Commands
{
    public class GetShoppingCartItem : IRequest<ShoppingCartItemResponse?>
    {
        #region Ctor

        public GetShoppingCartItem(long id)
        {
            Id = id;
        }

        public GetShoppingCartItem(Guid userId, long productId)
        {
            UserId = userId;
            ProductId = productId;
        }

        #endregion

        #region Properties

        public long? Id { get; set; }

        public Guid? UserId { get; set; }

        public long? ProductId { get; set; }

        #endregion
    }
}
