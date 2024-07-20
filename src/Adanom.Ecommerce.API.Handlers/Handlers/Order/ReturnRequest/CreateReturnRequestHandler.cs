namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateReturnRequestHandler : IRequestHandler<CreateReturnRequest, ReturnRequestResponse?>

    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateReturnRequestHandler(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ReturnRequestResponse?> Handle(CreateReturnRequest command, CancellationToken cancellationToken)
        {
            var orderResponse = await _mediator.Send(new GetOrder(command.Identity, command.OrderId));

            if (orderResponse == null)
            {
                return null;
            }

            var returnRequest = _mapper.Map<CreateReturnRequest, ReturnRequest>(command, options =>
            {
                options.AfterMap(async (source, target) =>
                {
                    target.OrderId = command.OrderId;
                    target.DeliveryType = orderResponse.DeliveryType.Key;
                    target.PickUpStoreId = orderResponse.PickUpStoreId;
                    target.ShippingProviderId = orderResponse.ShippingProviderId;
                    target.ReturnRequestNumber = await _mediator.Send(new CreateReturnRequestNumber());
                    target.ReturnRequestStatusType = ReturnRequestStatusType.RECEIVED;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            var returnRequestResponse = _mapper.Map<ReturnRequestResponse>(returnRequest);

            return returnRequestResponse;
        }

        #endregion
    }
}
