namespace Adanom.Ecommerce.API.Data.Models
{
    public class ProductPrice : BaseEntity<long>
    {
        public long TaxCategoryId { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal? DiscountedPrice { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public TaxCategory TaxCategory { get; set; } = null!;
    }
}
