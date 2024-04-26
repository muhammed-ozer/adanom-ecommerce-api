using Adanom.Ecommerce.API.Data.Models;
using MediatR;

namespace Adanom.Ecommerce.API.Commands
{
    public class SendMail : INotification
    {
        public MailTemplateKey Key { get; set; }

        public string To { get; set; } = null!;

        public string? Cc { get; set; }

        public string? Bcc { get; set; }

        public IDictionary<string, string>? Replacements { get; set; }

        public IEnumerable<UploadedFile>? Attachments { get; set; }

    }
}