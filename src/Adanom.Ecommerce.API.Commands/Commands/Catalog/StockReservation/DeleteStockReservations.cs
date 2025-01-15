namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteStockReservations : IRequest<bool>
    {
        #region Ctor

        public DeleteStockReservations(long orderId, bool? decreaseProductStockQuantity = null)
        {
            OrderId = orderId;
            DecreaseProductStockQuantity = decreaseProductStockQuantity;
        }

        #endregion

        #region Properties

        public long OrderId { get; set; }

        public bool? DecreaseProductStockQuantity { get; set; }

        #endregion
    }
}
