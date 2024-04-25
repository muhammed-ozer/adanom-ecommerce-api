using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(MetaInformationId), nameof(ProductCategoryId), IsUnique = true)]
    public class ProductCategory_MetaInformation_Mapping : IBaseEntity<long>
    {
        public long MetaInformationId { get; set; }

        public long ProductCategoryId { get; set; }

        public MetaInformation MetaInformation { get; set; } = null!;

        public ProductCategory ProductCategory { get; set; } = null!;
    }
}
