namespace Adanom.Ecommerce.API.Handlers
{
    public class RegisterUser_CreateNotificationBehavior : IPipelineBehavior<RegisterUser, bool>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public RegisterUser_CreateNotificationBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(RegisterUser command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var response = await next();

            if (response)
            {
                await _mediator.Publish(new CreateNotification(
                    NotificationType.NEW_USER,
                    string.Format(NotificationConstants.NewUser, $"{command.FirstName} {command.LastName}", command.Email)));
            }

            return response;
        }

        #endregion
    }
}
