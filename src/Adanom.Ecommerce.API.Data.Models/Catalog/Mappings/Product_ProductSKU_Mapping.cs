using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(ProductSKUId), nameof(ProductId), IsUnique = true)]
    public class Product_ProductSKU_Mapping
    {
        public long ProductId { get; set; }

        public long ProductSKUId { get; set; }

        public Product Product { get; set; } = null!;

        public ProductSKU ProductSKU { get; set; } = null!;
    }
}
