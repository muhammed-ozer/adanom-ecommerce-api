namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateReturnRequest_ItemRequest
    {
        public long OrderItemId { get; set; }

        public int Amount { get; set; }

        public string Description { get; set; } = null!;
    }
}