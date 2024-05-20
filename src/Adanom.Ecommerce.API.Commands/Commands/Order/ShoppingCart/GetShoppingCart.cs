namespace Adanom.Ecommerce.API.Commands
{
    public class GetShoppingCart : IRequest<ShoppingCartResponse?>
    {
        #region Ctor

        public GetShoppingCart(long id)
        {
            Id = id;
        }

        public GetShoppingCart(Guid userId)
        {
            UserId = userId;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public Guid? UserId { get; set; }

        #endregion
    }
}
