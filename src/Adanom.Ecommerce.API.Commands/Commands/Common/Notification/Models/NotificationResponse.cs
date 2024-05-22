namespace Adanom.Ecommerce.API.Commands.Models
{
    public sealed class NotificationResponse : BaseEntity<long>
    {
        public NotificationTypeResponse NotificationType { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? ReadAtUtc { get; set; }

        public Guid? ReadByUserId { get; set; }

        public UserResponse? ReadByUser { get; set; }
    }
}
