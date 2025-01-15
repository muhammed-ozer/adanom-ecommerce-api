namespace Adanom.Ecommerce.API.Commands
{
    public class GetStockReservationExpiresAtByOrderPaymentType : IRequest<DateTime>
    {
        #region Ctor

        public GetStockReservationExpiresAtByOrderPaymentType(OrderPaymentType orderPaymentType, DateTime reservedAtUtc)
        {
            ReservedAtUtc = reservedAtUtc;
            OrderPaymentType = orderPaymentType;
        }

        #endregion

        #region Properties

        public DateTime ReservedAtUtc { get; set; }

        public OrderPaymentType OrderPaymentType { get; set; }

        #endregion
    }
}
