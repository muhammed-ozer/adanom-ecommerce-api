using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class SliderItem : IBaseEntity<long>
    {
        public SliderItemType SliderItemType { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(500)]
        public string ImagePath { get; set; } = null!;

        [StringLength(1000)]
        public string? Url { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }
    }
}
