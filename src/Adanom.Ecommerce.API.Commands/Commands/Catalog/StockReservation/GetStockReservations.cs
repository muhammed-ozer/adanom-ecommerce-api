namespace Adanom.Ecommerce.API.Commands
{
    public class GetStockReservations : IRequest<IEnumerable<StockReservationResponse>>
    {
        #region Ctor

        public GetStockReservations(long orderId)
        {
            OrderId = orderId;
        }

        #endregion

        #region Properties

        public long OrderId { get; set; }

        #endregion
    }
}
