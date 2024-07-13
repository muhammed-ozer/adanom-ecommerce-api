namespace Adanom.Ecommerce.API.Commands
{
    public class CanUserAddProductToStockNotificationItems : IRequest<bool>
    {
        #region Ctor

        public CanUserAddProductToStockNotificationItems(Guid userId, long productId)
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