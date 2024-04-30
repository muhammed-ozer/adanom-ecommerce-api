using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class TaxCategory : BaseEntity<long>
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(100)]
        public string GroupName { get; set; } = null!;

        public byte Rate { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }
    }
}
