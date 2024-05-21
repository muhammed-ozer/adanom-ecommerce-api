namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderStatusTypeHandler : IRequestHandler<GetOrderStatusType, OrderStatusTypeResponse>

    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetOrderStatusTypeHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderStatusTypeResponse> Handle(GetOrderStatusType command, CancellationToken cancellationToken)
        {
            var orderStatusTypes = await _mediator.Send(new GetOrderStatusTypes());

            return orderStatusTypes.Single(e => e.Key == command.OrderStatusType);
        }

        #endregion
    }
}
