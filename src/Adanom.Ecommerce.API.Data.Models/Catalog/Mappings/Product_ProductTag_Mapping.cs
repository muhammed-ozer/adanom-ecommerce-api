using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(ProductTagId), nameof(ProductId), IsUnique = true)]
    public class Product_ProductTag_Mapping : BaseEntity<long>
    {
        public long ProductId { get; set; }

        public long ProductTagId { get; set; }

        public Product Product { get; set; } = null!;

        public ProductTag ProductTag { get; set; } = null!;
    }
}
