namespace Adanom.Ecommerce.API.Commands
{
    public class GetStockReservation : IRequest<StockReservationResponse?>
    {
        #region Ctor

        public GetStockReservation(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}
