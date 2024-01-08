using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Adanom.Ecommerce.Services.Mail
{
    internal sealed class MailService : IMailService
    {
        #region Fields

        private readonly IConfiguration _configuration;

        #endregion

        #region Ctor

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        #endregion

        #region IMailService Members

        public async Task SendAsync(MailRequest request)
        {
            var mail = new MimeMessage()
            { 
                Subject = request.Subject,
            };

            mail.To.Add(MailboxAddress.Parse(request.To));

            if (request.Cc.IsNotNullOrEmpty())
            {
                mail.Cc.Add(MailboxAddress.Parse(request.Cc));
            }

            if (request.Bcc.IsNotNullOrEmpty())
            {
                mail.Cc.Add(MailboxAddress.Parse(request.Bcc));
            }

            var builder = new BodyBuilder();

            builder.HtmlBody = request.Content;

            mail.Body = builder.ToMessageBody();

            if (request.Attachments is not null)
            {
                foreach (var attachment in request.Attachments)
                {
                    builder.Attachments.Add(attachment.Name, attachment.Content, ContentType.Parse(attachment.Content));
                }
            }

            await SenMailAsync(mail);
        }

        #endregion

        #region PrivateMethods

        private async Task SenMailAsync(MimeMessage mail)
        {
            var mailSettings = _configuration.GetSection("MailSettings").Get<MailSettings>();

            mail.Sender = MailboxAddress.Parse(mailSettings!.Mail);

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(mailSettings.Mail, mailSettings.Password);

            await smtp.SendAsync(mail);
        }

        #endregion
    }
}
