namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetDeliveryTypeHandler : IRequestHandler<GetDeliveryType, DeliveryTypeResponse>

    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetDeliveryTypeHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<DeliveryTypeResponse> Handle(GetDeliveryType command, CancellationToken cancellationToken)
        {
            var deliveryTypes = await _mediator.Send(new GetDeliveryTypes());

            return deliveryTypes.Single(e => e.Key == command.DeliveryType);
        }

        #endregion
    }
}
