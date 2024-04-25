using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(ProductSpecificationAttributeOptionId), nameof(ProductId), IsUnique = true)]
    public class Product_ProductSpecificationAttributeOption_Mapping : IBaseEntity<long>
    {
        public long ProductSpecificationAttributeOptionId { get; set; }

        public long ProductId { get; set; }

        public ProductSpecificationAttributeOption ProductSpecificationAttributeOption { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}
