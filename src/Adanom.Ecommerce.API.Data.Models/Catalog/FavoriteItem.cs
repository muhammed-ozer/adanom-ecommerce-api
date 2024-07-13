namespace Adanom.Ecommerce.API.Data.Models
{
    public class FavoriteItem : BaseEntity<long>
    {
        public Guid UserId { get; set; }

        public long ProductId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Product Product { get; set; } = null!;
    }
}
