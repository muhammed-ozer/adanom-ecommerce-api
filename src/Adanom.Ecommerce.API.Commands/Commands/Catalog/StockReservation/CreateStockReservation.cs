namespace Adanom.Ecommerce.API.Commands
{
    public class CreateStockReservation : IRequest<bool>
    {
        #region Ctor

        public CreateStockReservation()
        {
        }

        #endregion

        #region Properties

        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public int Amount { get; set; }

        public DateTime ReservedAtUtc { get; set; }

        public DateTime ExpiresAtUtc { get; set; }

        #endregion
    }
}
