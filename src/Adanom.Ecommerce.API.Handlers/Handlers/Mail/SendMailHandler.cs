using System.Text;
using Adanom.Ecommerce.API.Services.Mail;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class SendMailHandler : INotificationHandler<SendMail>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMailService _mailService;

        #endregion

        #region Ctor

        public SendMailHandler(
            ApplicationDbContext applicationDbContext,
            IMailService mailService)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        #endregion

        #region INotificationHandler Members

        public async Task Handle(SendMail command, CancellationToken cancellationToken)
        {
            var mailTemplate = await _applicationDbContext.MailTemplates
                .SingleOrDefaultAsync(e => e.Key == command.Key);

            if (mailTemplate is null)
            {
                throw new NullReferenceException(nameof(mailTemplate));
            }

            var request = new MailRequest()
            {
                To = command.To,
                Subject = mailTemplate.Subject,
                Bcc = command.Bcc,
                Cc = command.Cc,
                Attachments = command.Attachments
            };

            var content = new StringBuilder(mailTemplate.Content);

            if (command.Replacements is not null)
            {
                foreach (var replacement in command.Replacements)
                {
                    content.Replace(replacement.Key, replacement.Value);
                }
            }

            request.Content = content.ToString();

            await _mailService.SendAsync(request);
        } 

        #endregion
    }
}
