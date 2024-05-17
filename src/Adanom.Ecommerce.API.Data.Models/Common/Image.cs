using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class Image : BaseEntity<long>
    {
        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }

        public ImageType ImageType { get; set; }

        [StringLength(250)]
        public string Name { get; set; } = null!;

        [StringLength(1000)]
        public string Path { get; set; } = null!;

        public bool IsDefault { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }
    }
}
