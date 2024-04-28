using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class ProductSKU : BaseEntity<long>
    {
        public long ProductId { get; set; }

        public long? ProductAttributeOptionId { get; set; }

        public long ProductPriceId { get; set; }

        [StringLength(100)]
        public string Code { get; set; } = null!;

        public int StockQuantity { get; set; }

        public StockUnitType StockUnitType { get; set; }

        [StringLength(1000)]
        public string? Barcodes { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public Product Product { get; set; } = null!;

        public ProductPrice ProductPrice { get; set; } = null!;

        public ProductAttributeOption? ProductAttributeOption { get; set; }
    }
}
