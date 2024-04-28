using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class MailTemplate : BaseEntity<long>
    {
        public MailTemplateKey Key { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = null!;

        [StringLength(150)]
        public string Subject { get; set; } = null!;

        [StringLength(5000)]
        public string Content { get; set; } = null!;
    }
}
