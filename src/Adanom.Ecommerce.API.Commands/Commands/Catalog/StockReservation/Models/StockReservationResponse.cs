namespace Adanom.Ecommerce.API.Commands.Models
{
    public class StockReservationResponse : BaseResponseEntity<long>
    {
        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public int Amount { get; set; }

        public DateTime ReservedAtUtc { get; set; }

        public DateTime ExpiresAtUtc { get; set; }
    }
}