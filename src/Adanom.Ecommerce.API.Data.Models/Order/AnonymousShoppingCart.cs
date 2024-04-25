using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(LastModifiedAtUtc), IsUnique = false)]
    public class AnonymousShoppingCart : IBaseEntity<Guid>
    {
        public AnonymousShoppingCart()
        {
            Items = new List<AnonymousShoppingCartItem>();
        }

        public DateTime LastModifiedAtUtc { get; set; }

        public ICollection<AnonymousShoppingCartItem> Items { get; set; }
    }
}
