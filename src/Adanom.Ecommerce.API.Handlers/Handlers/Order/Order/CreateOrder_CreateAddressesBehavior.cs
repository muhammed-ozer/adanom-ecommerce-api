namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrder_CreateAddressesBehavior : IPipelineBehavior<CreateOrder, OrderResponse?>
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateOrder_CreateAddressesBehavior(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<OrderResponse?> Handle(CreateOrder command, RequestHandlerDelegate<OrderResponse?> next, CancellationToken cancellationToken)
        {
            var orderResponse = await next();

            if (orderResponse == null)
            {
                return null;
            }

            var shippingAddressResponse = await _mediator.Send(new GetShippingAddress(command.ShippingAddressId));

            if (shippingAddressResponse == null)
            {
                return null;
            }

            var createOrderShippingAddressRequest = _mapper.Map<CreateOrderShippingAddressRequest>(shippingAddressResponse);

            var shippingAddress_AddressCityResponse = await _mediator.Send(new GetAddressCity(shippingAddressResponse.AddressCityId));
            var shippingAddress_AddressDistrictResponse = await _mediator.Send(new GetAddressDistrict(shippingAddressResponse.AddressDistrictId));

            createOrderShippingAddressRequest.AddressCityName = shippingAddress_AddressCityResponse?.Name ?? "";
            createOrderShippingAddressRequest.AddressDistrictName = shippingAddress_AddressDistrictResponse?.Name ?? "";

            var createOrderShippingAddressCommand = _mapper.Map(createOrderShippingAddressRequest, new CreateOrderShippingAddress(command.Identity));

            var orderShippingAddressResponse = await _mediator.Send(createOrderShippingAddressCommand);

            if (orderShippingAddressResponse == null)
            {
                return null;
            }

            orderResponse.OrderShippingAddressId = orderShippingAddressResponse.Id;

            if (command.BillingAddressId != null)
            {
                var billingAddressResponse = await _mediator.Send(new GetBillingAddress(command.BillingAddressId.Value));

                if (billingAddressResponse == null)
                {
                    return null;
                }

                var createOrderBillingAddressRequest = _mapper.Map<CreateOrderBillingAddressRequest>(billingAddressResponse);

                var billingAddress_AddressCityResponse = await _mediator.Send(new GetAddressCity(billingAddressResponse.AddressCityId));
                var billingAddress_AddressDistrictResponse = await _mediator.Send(new GetAddressDistrict(billingAddressResponse.AddressDistrictId));

                createOrderBillingAddressRequest.AddressCityName = billingAddress_AddressCityResponse?.Name ?? "";
                createOrderBillingAddressRequest.AddressDistrictName = billingAddress_AddressDistrictResponse?.Name ?? "";

                var createBillingAddressCommand = _mapper.Map(createOrderBillingAddressRequest, new CreateOrderBillingAddress(command.Identity));

                var orderBillingAddressResponse = await _mediator.Send(createBillingAddressCommand);

                if (orderBillingAddressResponse == null)
                {
                    return null;
                }

                orderResponse.OrderBillingAddressId = orderBillingAddressResponse.Id;
            }

            return orderResponse;
        }

        #endregion
    }
}
