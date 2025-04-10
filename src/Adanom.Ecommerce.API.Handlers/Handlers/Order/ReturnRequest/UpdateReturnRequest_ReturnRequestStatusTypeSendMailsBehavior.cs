﻿using Adanom.Ecommerce.API.Services.Mail;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateReturnRequest_ReturnRequestStatusTypeSendMailsBehavior : IPipelineBehavior<UpdateReturnRequest_ReturnRequestStatusType, bool>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateReturnRequest_ReturnRequestStatusTypeSendMailsBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(UpdateReturnRequest_ReturnRequestStatusType command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var updateReturnRequest_ReturnRequestStatusTypeResponse = await next();

            if (!updateReturnRequest_ReturnRequestStatusTypeResponse)
            {
                return updateReturnRequest_ReturnRequestStatusTypeResponse;
            }

            if (command.OldReturnRequestStatusType == command.NewReturnRequestStatusType)
            {
                return updateReturnRequest_ReturnRequestStatusTypeResponse;
            }

            if (command.NewReturnRequestStatusType == ReturnRequestStatusType.IN_PROGRESS)
            {
                return updateReturnRequest_ReturnRequestStatusTypeResponse;
            }

            var returnRequest = await _mediator.Send(new GetReturnRequest(command.Id));

            if (returnRequest == null)
            {
                return updateReturnRequest_ReturnRequestStatusTypeResponse;
            }

            var order = await _mediator.Send(new GetOrder(returnRequest.OrderId));

            if (order == null)
            {
                return updateReturnRequest_ReturnRequestStatusTypeResponse;
            }

            var user = await _mediator.Send(new GetUser(order.UserId));

            if (user == null)
            {
                return updateReturnRequest_ReturnRequestStatusTypeResponse;
            }

            var sendMailCommand = new SendMail()
            {
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.ReturnRequest.Number, returnRequest.ReturnRequestNumber }
                }
            };

            sendMailCommand.Key = returnRequest.ReturnRequestStatusType.Key switch
            {
                ReturnRequestStatusType.RECEIVED => MailTemplateKey.RETURN_REQUEST_RECEIVED,
                ReturnRequestStatusType.APPROVED => MailTemplateKey.RETURN_REQUEST_APPROVED,
                ReturnRequestStatusType.REFUND_MADE => MailTemplateKey.RETURN_REQUEST_REFUND_MADE,
                ReturnRequestStatusType.DISAPPROVED => MailTemplateKey.RETURN_REQUEST_DISAPPROVED,
                ReturnRequestStatusType.CANCEL => MailTemplateKey.RETURN_REQUEST_CANCEL,
                _ => throw new Exception("Invalid return request status type")
            };

            if (returnRequest.ReturnRequestStatusType.Key == ReturnRequestStatusType.DISAPPROVED)
            {
                sendMailCommand.Replacements.Add(
                            new KeyValuePair<string, string>(
                                MailConstants.Replacements.ReturnRequest.DisapprovedReasonMessage, returnRequest.DisapprovedReasonMessage!));
            }

            await _mediator.Publish(sendMailCommand);

            return updateReturnRequest_ReturnRequestStatusTypeResponse;
        }

        #endregion
    }
}
