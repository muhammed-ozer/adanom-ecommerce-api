namespace Adanom.Ecommerce.API.Commands.Models
{
    public class OrderItemResponse : BaseResponseEntity<long>
    {
        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public long ProductSKUId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public string AmountUnit { get; set; } = null!;

        public byte TaxRate { get; set; }

        public decimal TaxTotal { get; set; }

        public byte DiscountRate { get; set; }

        public decimal DiscountTotal { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public OrderResponse? Order { get; set; }

        public ProductResponse? Product { get; set; }

        public ProductSKUResponse? ProductSKU { get; set; }
    }
}