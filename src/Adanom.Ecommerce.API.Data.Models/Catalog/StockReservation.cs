namespace Adanom.Ecommerce.API.Data.Models
{
    public class StockReservation : BaseEntity<long>
    {
        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public int Amount { get; set; }

        public DateTime ReservedAtUtc { get; set; }

        public DateTime ExpiresAtUtc { get; set; }

        public Order Order { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}
