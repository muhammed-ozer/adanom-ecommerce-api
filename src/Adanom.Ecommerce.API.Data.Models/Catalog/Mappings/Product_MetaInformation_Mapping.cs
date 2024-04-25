using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(MetaInformationId), nameof(ProductId), IsUnique = true)]
    public class Product_MetaInformation_Mapping : IBaseEntity<long>
    {
        public long MetaInformationId { get; set; }

        public long ProductId { get; set; }

        public MetaInformation MetaInformation { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}
