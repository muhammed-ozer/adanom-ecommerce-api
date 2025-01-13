namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAllowedPaymentTypesByDeliveryTypeHandler : IRequestHandler<GetAllowedPaymentTypesByDeliveryType, IEnumerable<OrderPaymentTypeResponse>>

    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetAllowedPaymentTypesByDeliveryTypeHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<OrderPaymentTypeResponse>> Handle(GetAllowedPaymentTypesByDeliveryType command, CancellationToken cancellationToken)
        {
            var orderPaymentTypes = await _mediator.Send(new GetOrderPaymentTypes());

            return command.DeliveryType switch
            {
                DeliveryType.PICK_UP_FROM_STORE => orderPaymentTypes,
                DeliveryType.LOCAL_DELIVERY => orderPaymentTypes,
                DeliveryType.CARGO_SHIPMENT => orderPaymentTypes
                    .Where(e => e.Key == OrderPaymentType.ONLINE_CREDIT_CARD ||
                                e.Key == OrderPaymentType.BANK_TRANSFER)
                    .ToList(),
                _ => throw new ArgumentException($"Unsupported delivery type: {command.DeliveryType}")
            };
        }

        #endregion
    }
}
