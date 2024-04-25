using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(MetaInformationId), nameof(BrandId), IsUnique = true)]
    public class Brand_MetaInformation_Mapping : IBaseEntity<long>
    {
        public long MetaInformationId { get; set; }

        public long BrandId { get; set; }

        public MetaInformation MetaInformation { get; set; } = null!;

        public Brand Brand { get; set; } = null!;
    }
}
