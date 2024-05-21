using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Services.Mail
{
    public sealed class MailRequest
    {
        public string To { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string? Cc { get; set; }

        public string? Bcc { get; set; }

        public IEnumerable<UploadedFile>? Attachments { get; set; }
    }
}
