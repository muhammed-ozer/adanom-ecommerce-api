namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateOrderPayment_UpdateOrderStatusTypeBehavior : IPipelineBehavior<UpdateOrderPayment, bool>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public UpdateOrderPayment_UpdateOrderStatusTypeBehavior(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(UpdateOrderPayment command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var updateOrderPaymentResponse = await next();

            if (!updateOrderPaymentResponse)
            {
                return updateOrderPaymentResponse;
            }

            var order = await _mediator.Send(new GetOrder(command.OrderId));
            var orderPayment = await _mediator.Send(new GetOrderPayment(command.Id));

            if (!orderPayment!.Paid)
            {
                return updateOrderPaymentResponse;
            }

            if (orderPayment.OrderPaymentType.Key == OrderPaymentType.BANK_TRANSFER && command.Paid)
            {
                var updateOrderStatusTypeRequest = new UpdateOrder_OrderStatusTypeRequest()
                {
                    Id = order!.Id,
                    OrderPaymentType = orderPayment.OrderPaymentType.Key,
                    NewOrderStatusType = OrderStatusType.APPROVED,
                    DeliveryType = order.DeliveryType.Key,
                };

                var updateOrderStatusTypeCommand = _mapper.Map(updateOrderStatusTypeRequest, new UpdateOrder_OrderStatusType(command.Identity));
                await _mediator.Send(updateOrderStatusTypeCommand);
            }

            if (orderPayment.OrderPaymentType.Key == OrderPaymentType.ONLINE_PAYMENT && command.Paid)
            {
                var updateOrderStatusTypeRequest = new UpdateOrder_OrderStatusTypeRequest()
                {
                    Id = order!.Id,
                    OrderPaymentType = orderPayment.OrderPaymentType.Key,
                    NewOrderStatusType = OrderStatusType.NEW,
                    DeliveryType = order.DeliveryType.Key,
                };

                var updateOrderStatusTypeCommand = _mapper.Map(updateOrderStatusTypeRequest, new UpdateOrder_OrderStatusType(command.Identity));
                await _mediator.Send(updateOrderStatusTypeCommand);
            }

            if (orderPayment.OrderPaymentType.Key == OrderPaymentType.CASH_ON_DELIVERY || orderPayment.OrderPaymentType.Key == OrderPaymentType.CREDIT_CARD_ON_DELIVERY)
            {
                if (command.Paid)
                {
                    var updateOrderStatusTypeRequest = new UpdateOrder_OrderStatusTypeRequest()
                    {
                        Id = order!.Id,
                        OrderPaymentType = orderPayment.OrderPaymentType.Key,
                        NewOrderStatusType = OrderStatusType.DONE,
                        DeliveryType = order.DeliveryType.Key,
                    };

                    var updateOrderStatusTypeCommand = _mapper.Map(updateOrderStatusTypeRequest, new UpdateOrder_OrderStatusType(command.Identity));
                    await _mediator.Send(updateOrderStatusTypeCommand);
                }
            }

            return updateOrderPaymentResponse;
        }

        #endregion
    }
}
