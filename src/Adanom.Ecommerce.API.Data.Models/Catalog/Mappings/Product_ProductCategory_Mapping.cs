using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(ProductCategoryId), nameof(ProductId), IsUnique = true)]
    public class Product_ProductCategory_Mapping : IBaseEntity<long>
    {
        public long ProductId { get; set; }

        public long ProductCategoryId { get; set; }

        public Product Product { get; set; } = null!;

        public ProductCategory ProductCategory { get; set; } = null!;
    }
}
