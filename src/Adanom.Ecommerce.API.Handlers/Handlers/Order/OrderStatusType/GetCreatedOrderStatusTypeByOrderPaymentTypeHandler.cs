namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetCreatedOrderStatusTypeByOrderPaymentTypeHandler : IRequestHandler<GetCreatedOrderStatusTypeByOrderPaymentType, OrderStatusTypeResponse>

    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetCreatedOrderStatusTypeByOrderPaymentTypeHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderStatusTypeResponse> Handle(GetCreatedOrderStatusTypeByOrderPaymentType command, CancellationToken cancellationToken)
        {
            var orderStatusTypes = await _mediator.Send(new GetOrderStatusTypes());

            return command.OrderPaymentType switch
            {
                OrderPaymentType.ONLINE_PAYMENT => orderStatusTypes.Single(e => e.Key == OrderStatusType.PAYMENT_PENDING),
                OrderPaymentType.BANK_TRANSFER => orderStatusTypes.Single(e => e.Key == OrderStatusType.PAYMENT_PENDING),
                OrderPaymentType.CREDIT_CARD_ON_DELIVERY => orderStatusTypes.Single(e => e.Key == OrderStatusType.NEW),
                OrderPaymentType.CASH_ON_DELIVERY => orderStatusTypes.Single(e => e.Key == OrderStatusType.NEW),
                _ => throw new ArgumentException($"Unsupported order payment type: {command.OrderPaymentType}")
            };
        }

        #endregion
    }
}
