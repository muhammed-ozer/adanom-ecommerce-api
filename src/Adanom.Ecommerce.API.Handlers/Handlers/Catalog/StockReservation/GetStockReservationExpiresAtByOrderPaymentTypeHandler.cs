namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetStockReservationExpiresAtByOrderPaymentTypeHandler : IRequestHandler<GetStockReservationExpiresAtByOrderPaymentType, DateTime>
    {
        #region Fields


        #endregion

        #region Ctor

        public GetStockReservationExpiresAtByOrderPaymentTypeHandler()
        {
        }

        #endregion

        #region IRequestHandler Members

        public Task<DateTime> Handle(GetStockReservationExpiresAtByOrderPaymentType command, CancellationToken cancellationToken)
        {
            var expiresAtUtc = command.OrderPaymentType switch
            {
                OrderPaymentType.ONLINE_PAYMENT => command.ReservedAtUtc.AddDays(2),
                OrderPaymentType.BANK_TRANSFER => command.ReservedAtUtc.AddDays(3),
                OrderPaymentType.CREDIT_CARD_ON_DELIVERY => command.ReservedAtUtc.AddDays(3),
                OrderPaymentType.CASH_ON_DELIVERY => command.ReservedAtUtc.AddDays(3),
                _ => DateTime.UtcNow.AddMinutes(15)
            };

            return Task.FromResult(expiresAtUtc);
        } 

        #endregion
    }
}
