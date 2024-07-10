using Adanom.Ecommerce.API.Services.Mail;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateOrder_OrderStatusTypeSendMailsBehavior : IPipelineBehavior<UpdateOrder_OrderStatusType, bool>
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IMailService _mailServide;

        #endregion

        #region Ctor

        public UpdateOrder_OrderStatusTypeSendMailsBehavior(
            IMapper mapper,
            IMediator mediator,
            IMailService mailService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mailServide = mailService ?? throw new ArgumentNullException(nameof(mailService));
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

            var order = await _mediator.Send(new GetOrder(command.Id));

            if (order == null)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            var orderStatusType = order.OrderStatusType.Key;
            var deliveryType = order.DeliveryType.Key;

            if (orderStatusType == command.OldOrderStatusType || orderStatusType == OrderStatusType.NEW)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            var user = await _mediator.Send(new GetUser(order.UserId));

            if (user == null)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            var sendMailCommand = new SendMail()
            {
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Order.Number, order.OrderNumber }
                }
            };

            if (orderStatusType == OrderStatusType.IN_PROGRESS)
            {
                sendMailCommand.Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_IN_PROGRESS;
            }
            else if (orderStatusType == OrderStatusType.READY)
            {
                if (deliveryType == DeliveryType.PICK_UP_FROM_STORE)
                {
                    sendMailCommand.Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_READY_PICK_UP_FROM_STORE;
                }
            }
            else if (orderStatusType == OrderStatusType.DELIVERED_TO_SHIPPING_PROVIDER)
            {
                if (deliveryType == DeliveryType.CARGO_SHIPMENT)
                {
                    var shippingProvider = await _mediator.Send(new GetShippingProvider(order.ShippingProviderId!.Value));

                    if (shippingProvider != null)
                    {
                        sendMailCommand.Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_DELIVERED_TO_SHIPPING_PROVIDER_CARGO_SHIPMENT;

                        sendMailCommand.Replacements.Add(
                            new KeyValuePair<string, string>(MailConstants.Replacements.Order.ShippingTrackingCode, order.ShippingTrackingCode ?? "-"));

                        sendMailCommand.Replacements.Add(
                            new KeyValuePair<string, string>(MailConstants.Replacements.Order.ShippingProviderName, shippingProvider.DisplayName));
                    }
                }
            }
            else if (orderStatusType == OrderStatusType.DELIVERED_TO_CUSTOMER)
            {
                sendMailCommand.Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_DELIVERED_TO_CUSTOMER;
            }
            else if (orderStatusType == OrderStatusType.CANCEL)
            {
                sendMailCommand.Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_CANCEL;
            }

            if (sendMailCommand.Key == MailTemplateKey.AUTH_NEW_USER)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            await _mediator.Publish(sendMailCommand);

            return updateOrder_OrderStatusTypeResponse;
        }

        #endregion
    }
}
