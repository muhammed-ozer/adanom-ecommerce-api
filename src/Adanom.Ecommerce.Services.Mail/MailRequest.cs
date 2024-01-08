using Adanom.Ecommerce.Data.Models;

namespace Adanom.Ecommerce.Services.Mail
{
    public sealed class MailRequest
    {
        public MailTemplateKey Key { get; set; }

        public string To { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string? Cc { get; set; }

        public string? Bcc { get; set; }

        public IDictionary<string, string>? Replacements { get; set; }

        public IEnumerable<UploadedFile>? Attachments { get; set; }
    }
}
