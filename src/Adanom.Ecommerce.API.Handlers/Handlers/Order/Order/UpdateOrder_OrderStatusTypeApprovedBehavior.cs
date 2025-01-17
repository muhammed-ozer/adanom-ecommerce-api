namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateOrder_OrderStatusTypeApprovedBehavior : IPipelineBehavior<UpdateOrder_OrderStatusType, bool>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateOrder_OrderStatusTypeApprovedBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(UpdateOrder_OrderStatusType command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var updateOrder_OrderStatusTypeResponse = await next();

            if (!updateOrder_OrderStatusTypeResponse)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            if (command.OldOrderStatusType == command.NewOrderStatusType)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            if (command.NewOrderStatusType != OrderStatusType.APPROVED)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            var order = await _mediator.Send(new GetOrder(command.Id));

            if (order == null)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            if (command.OrderPaymentType == OrderPaymentType.BANK_TRANSFER || 
                command.OrderPaymentType == OrderPaymentType.CREDIT_CARD_ON_DELIVERY || 
                command.OrderPaymentType == OrderPaymentType.CASH_ON_DELIVERY)
            {
                // Delete stock reservations when order approved and order payment type not equals to online payment
                await _mediator.Send(new DeleteStockReservations(order.Id, true));
            }

            return updateOrder_OrderStatusTypeResponse;
        }

        #endregion
    }
}
