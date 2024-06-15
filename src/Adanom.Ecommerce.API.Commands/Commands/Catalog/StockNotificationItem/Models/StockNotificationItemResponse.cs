namespace Adanom.Ecommerce.API.Commands.Models
{
    public class StockNotificationItemResponse : BaseResponseEntity<long>
    {
        public Guid UserId { get; set; }

        public long ProductId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? EmailSentAtUtc { get; set; }

        public ProductResponse? Product { get; set; }

        public ProductSKUResponse? ProductSKU { get; set; }

        public UserResponse? User { get; set; }
    }
}