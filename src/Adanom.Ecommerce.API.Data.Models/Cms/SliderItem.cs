using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class SliderItem : BaseEntity<long>
    {
        public long ImageId { get; set; }

        public SliderItemType SliderItemType { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(1000)]
        public string? Url { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public Image Image { get; set; } = null!;
    }
}
