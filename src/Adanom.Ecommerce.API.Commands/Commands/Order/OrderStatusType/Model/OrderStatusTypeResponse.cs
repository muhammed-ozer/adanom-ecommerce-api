namespace Adanom.Ecommerce.API.Commands.Models
{
    public sealed class OrderStatusTypeResponse
    {
        public OrderStatusTypeResponse(OrderStatusType key)
        {
            Key = key;
        }

        public OrderStatusType Key { get; }

        public string Name { get; set; } = null!;
    }
}