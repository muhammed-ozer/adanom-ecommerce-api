using Adanom.Ecommerce.API.Services.Mail;

namespace Adanom.Ecommerce.API.Handlers
{
    public class RegisterUser_SendMailsBehavior : IPipelineBehavior<RegisterUser, bool>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public RegisterUser_SendMailsBehavior(IMediator mediator)
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
                await _mediator.Publish(new SendMail()
                {
                    Key = MailTemplateKey.AUTH_NEW_USER,
                    To = command.Email,
                    Replacements = new Dictionary<string, string>()
                    {
                        { MailConstants.Replacements.User.FullName, $"{command.FirstName} {command.LastName}" }
                    }
                });

                await _mediator.Send(new SendEmailConfirmationEmail()
                {
                    Email = command.Email
                });
            }

            return response;
        }

        #endregion
    }
}
