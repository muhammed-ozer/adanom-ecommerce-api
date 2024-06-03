namespace Adanom.Ecommerce.API.Commands.Models
{
    public sealed class MarkAsReadNotificationsRequest
    {
        public MarkAsReadNotificationsRequest()
        {
            Ids = new List<int>();
        }

        public IEnumerable<int> Ids { get; set; }
    }
}
