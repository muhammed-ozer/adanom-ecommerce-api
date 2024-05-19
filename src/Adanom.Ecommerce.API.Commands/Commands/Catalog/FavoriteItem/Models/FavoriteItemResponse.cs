namespace Adanom.Ecommerce.API.Commands.Models
{
    public class FavoriteItemResponse : BaseResponseEntity<long>
    {
        public Guid UserId { get; set; }

        public long ProductId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public ProductResponse? Product { get; set; }

        public UserResponse? User { get; set; }
    }
}