using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class Brand : BaseEntity<long>
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(150)]
        public string UrlSlug { get; set; } = null!;

        [StringLength(1000)]
        public string? LogoPath { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }
    }
}
