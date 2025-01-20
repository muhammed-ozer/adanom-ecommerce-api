using System.Net.Mail;
using System.Net.Mime;
using Adanom.Ecommerce.API.Services.Mail;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateOrder_OrderStatusTypeNewBehavior : IPipelineBehavior<UpdateOrder_OrderStatusType, bool>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IPdfGeneratorService _pdfGeneratorService;

        #endregion

        #region Ctor

        public UpdateOrder_OrderStatusTypeNewBehavior(IMediator mediator, IPdfGeneratorService pdfGeneratorService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _pdfGeneratorService = pdfGeneratorService ?? throw new ArgumentNullException(nameof(pdfGeneratorService));
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

            if (command.NewOrderStatusType != OrderStatusType.NEW)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            var order = await _mediator.Send(new GetOrder(command.Id));

            if (order == null)
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
                Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_NEW,
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Order.Number, order.OrderNumber }
                }
            };

            var orderDocumentsResponse = await _mediator.Send(
            new Order_CreateOrderDocuments(
                user,
                order,
                command.OrderPaymentType,
                order.OrderShippingAddressId,
                order.OrderBillingAddressId));

            var distanceSellingContractPdf = _pdfGeneratorService.GeneratePdf(
            orderDocumentsResponse.DistanceSellingContractHtmlContent,
                $"Mesafeli Satış Sözleşmesi.pdf");

            var preliminaryInformationFormPdf = _pdfGeneratorService.GeneratePdf(
                orderDocumentsResponse.PreliminaryInformationFormHtmlContent,
                $"Ön Bilgilendirme Formu.pdf");

            sendMailCommand.Attachments =
            [
                new Attachment(new MemoryStream(distanceSellingContractPdf), "Mesafeli Satış Sözleşmesi.pdf", MediaTypeNames.Application.Pdf),
                new Attachment(new MemoryStream(preliminaryInformationFormPdf), "Ön Bilgilendirme Formu.pdf", MediaTypeNames.Application.Pdf),
            ];

            await _mediator.Publish(sendMailCommand);

            // Delete stock reservations and shopping cart when online payment successful
            if (command.OrderPaymentType == OrderPaymentType.ONLINE_PAYMENT)
            {
                await _mediator.Send(new DeleteShoppingCart(command.Identity));
                await _mediator.Send(new DeleteStockReservations(order.Id, true));
            }

            var deliveryType = await _mediator.Send(new GetDeliveryType(command.DeliveryType));
            var orderPaymentType = await _mediator.Send(new GetOrderPaymentType(command.OrderPaymentType));

            var sendToManagerMailCommand = new SendMail()
            {
                To = MailNotificationConstants.Receivers.Order,
                Key = MailTemplateKey.ADMIN_ORDER_RECEIVED,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Order.Number, order.OrderNumber },
                    { MailConstants.Replacements.Order.DeliveryType, deliveryType.Name },
                    { MailConstants.Replacements.Order.OrderPaymentType, orderPaymentType.Name },
                }
            };

            await _mediator.Publish(sendToManagerMailCommand);

            return updateOrder_OrderStatusTypeResponse;
        }

        #endregion
    }
}
