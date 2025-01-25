using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class ProductSKU : BaseEntity<long>
    {
        public ProductSKU()
        {
            Product_ProductSKU_Mappings = new List<Product_ProductSKU_Mapping>();
        }

        public long ProductPriceId { get; set; }

        [StringLength(100)]
        public string Code { get; set; } = null!;

        public int StockQuantity { get; set; }

        public StockUnitType StockUnitType { get; set; }

        [StringLength(1000)]
        public string? Barcodes { get; set; }

        public bool IsEligibleToInstallment { get; set; }

        public byte MaximumInstallmentCount { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public ProductPrice ProductPrice { get; set; } = null!;

        public ICollection<Product_ProductSKU_Mapping> Product_ProductSKU_Mappings { get; set; }
    }
}
