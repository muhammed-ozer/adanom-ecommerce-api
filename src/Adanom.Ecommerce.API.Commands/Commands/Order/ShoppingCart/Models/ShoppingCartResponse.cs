namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ShoppingCartResponse : BaseResponseEntity<long>
    {
        public ShoppingCartResponse()
        {
            Items = new List<ShoppingCartItemResponse>();
        }

        public Guid UserId { get; set; }

        public DateTime LastModifiedAtUtc { get; set; }

        public decimal Total { get; set; }

        public UserResponse? User { get; set; }

        public ICollection<ShoppingCartItemResponse> Items { get; set; }
    }
}