namespace Adanom.Ecommerce.API.Commands.Models
{
    public sealed class OrderPaymentTypeResponse
    {
        public OrderPaymentTypeResponse(OrderPaymentType key)
        {
            Key = key;
        }

        public OrderPaymentType Key { get; }

        public string Name { get; set; } = null!;

        public byte DiscountRate { get; set; }
    }
}