namespace Adanom.Ecommerce.API.Commands.Models
{
    public sealed class NotificationTypeResponse
    {
        public NotificationTypeResponse(NotificationType key)
        {
            Key = key;
        }

        public NotificationType Key { get; }

        public string Name { get; set; } = null!;
    }
}