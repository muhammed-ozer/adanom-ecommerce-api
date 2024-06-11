namespace Adanom.Ecommerce.API.Data.Models
{
    public class StockNotificationItem : BaseEntity<long>
    {
        public Guid UserId { get; set; }

        public long ProductId { get; set; }

        public long ProductSKUId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? EmailSentAtUtc { get; set; }
    }
}
