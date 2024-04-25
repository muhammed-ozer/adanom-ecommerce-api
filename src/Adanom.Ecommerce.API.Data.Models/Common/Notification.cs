using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class Notification : IBaseEntity<long>
    {
        public NotificationType NotificationType { get; set; }

        [StringLength(1000)]
        public string Content { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? ReadAtUtc { get; set; }

        public Guid? ReadByUserId { get; set; }
    }
}
