namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CancelOrder_DeleteStockReservationsBehavior : IPipelineBehavior<CancelOrder, bool>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CancelOrder_DeleteStockReservationsBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(CancelOrder command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var cancelOrderResponse = await next();

            if (!cancelOrderResponse)
            {
                return cancelOrderResponse;
            }

            await _mediator.Send(new DeleteStockReservations(command.Id, false));

            return cancelOrderResponse;
        }

        #endregion
    }
}
