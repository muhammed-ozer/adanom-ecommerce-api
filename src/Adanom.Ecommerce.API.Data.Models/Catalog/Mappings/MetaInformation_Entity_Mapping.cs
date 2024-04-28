using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(MetaInformationId), IsUnique = true)]
    public class MetaInformation_Entity_Mapping : IBaseEntity<long>
    {
        public long MetaInformationId { get; set; }

        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }
    }
}
