using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class TaxAdministration : BaseEntity<long>
    {
        [StringLength(50)]
        public string Code { get; set; } = null!;

        [StringLength(200)]
        public string Name { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreateByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }
    }
}
