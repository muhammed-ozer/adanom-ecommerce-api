using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(ProductSpecificationAttributeId), nameof(ProductId), IsUnique = true)]
    public class Product_ProductSpecificationAttribute_Mapping
    {
        public long ProductSpecificationAttributeId { get; set; }

        public long ProductId { get; set; }

        public ProductSpecificationAttribute ProductSpecificationAttribute { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}
