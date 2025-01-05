namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ReturnRequestItemResponse : BaseResponseEntity<long>
    {
        public long ReturnRequestId { get; set; }

        public long OrderItemId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public string AmountUnit { get; set; } = null!;

        public byte TaxRate { get; set; }

        public decimal Total { get; set; }

        public string Description { get; set; } = null!;

        public ReturnRequestResponse? ReturnRequest { get; set; }

        public OrderItemResponse? OrderItem { get; set; }
    }
}