namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetNotificationTypeHandler : IRequestHandler<GetNotificationType, NotificationTypeResponse>

    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetNotificationTypeHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<NotificationTypeResponse> Handle(GetNotificationType command, CancellationToken cancellationToken)
        {
            var notificationTypes = await _mediator.Send(new GetNotificationTypes());

            return notificationTypes.Single(e => e.Key == command.NotificationType);
        }

        #endregion
    }
}
