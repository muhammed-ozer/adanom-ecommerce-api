namespace Adanom.Ecommerce.API.Commands
{
    public class CanUserAddProductToFavoriteItems : IRequest<bool>
    {
        #region Ctor

        public CanUserAddProductToFavoriteItems(Guid userId, long productId)
        {
            UserId = userId;
            ProductId = productId;
        }

        #endregion

        #region Properties

        public Guid UserId { get; }

        public long ProductId { get; set; }

        #endregion
    }
}