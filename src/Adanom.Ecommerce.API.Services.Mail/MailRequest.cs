using System.Net.Mail;

namespace Adanom.Ecommerce.API.Services.Mail
{
    public sealed class MailRequest
    {
        public string To { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string? Cc { get; set; }

        public string? Bcc { get; set; }

        public IEnumerable<Attachment>? Attachments { get; set; }
    }
}
