namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderPaymentTypeHandler : IRequestHandler<GetOrderPaymentType, OrderPaymentTypeResponse>

    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetOrderPaymentTypeHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderPaymentTypeResponse> Handle(GetOrderPaymentType command, CancellationToken cancellationToken)
        {
            var orderPaymentTypes = await _mediator.Send(new GetOrderPaymentTypes());

            return orderPaymentTypes.Single(e => e.Key == command.OrderPaymentType);
        }

        #endregion
    }
}
