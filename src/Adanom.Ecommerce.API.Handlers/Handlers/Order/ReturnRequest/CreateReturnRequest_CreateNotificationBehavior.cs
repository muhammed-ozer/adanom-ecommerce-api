namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateReturnRequest_CreateNotificationBehavior : IPipelineBehavior<CreateReturnRequest, ReturnRequestResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateReturnRequest_CreateNotificationBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<ReturnRequestResponse?> Handle(CreateReturnRequest command, RequestHandlerDelegate<ReturnRequestResponse?> next, CancellationToken cancellationToken)
        {
            var returnRequestResponse = await next();

            if (returnRequestResponse == null)
            {
                return null;
            }

            await _mediator.Publish(new CreateNotification(
                NotificationType.NEW_RETURN_REQUEST,
                 string.Format(NotificationConstants.NewReturnRequest, returnRequestResponse.ReturnRequestNumber)));

            return returnRequestResponse;
        }

        #endregion
    }
}
