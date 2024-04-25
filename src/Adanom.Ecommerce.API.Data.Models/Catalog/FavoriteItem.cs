namespace Adanom.Ecommerce.API.Data.Models
{
    public class FavoriteItem : IBaseEntity<long>
    {
        public Guid UserId { get; set; }

        public long ProductId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Product Product { get; set; } = null!;
    }
}
