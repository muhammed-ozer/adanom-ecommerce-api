using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class MetaInformation : BaseEntity<long>
    {
        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }

        [StringLength(250)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string Description { get; set; } = null!;

        [StringLength(1000)]
        public string Keywords { get; set; } = null!;
    }
}
