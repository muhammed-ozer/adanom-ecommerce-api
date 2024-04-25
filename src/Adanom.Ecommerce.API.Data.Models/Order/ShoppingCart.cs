namespace Adanom.Ecommerce.API.Data.Models
{
    public class ShoppingCart : IBaseEntity<long>
    {
        public ShoppingCart()
        {
            Items = new List<ShoppingCartItem>();
        }

        public Guid UserId { get; set; }

        public DateTime LastModifiedAtUtc { get; set; }

        public ICollection<ShoppingCartItem> Items { get; set; }
    }
}
