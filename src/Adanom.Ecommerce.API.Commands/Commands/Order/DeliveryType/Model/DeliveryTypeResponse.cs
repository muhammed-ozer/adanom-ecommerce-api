namespace Adanom.Ecommerce.API.Commands.Models
{
    public sealed class DeliveryTypeResponse
    {
        public DeliveryTypeResponse(DeliveryType key)
        {
            Key = key;
        }

        public DeliveryType Key { get; }

        public string Name { get; set; } = null!;
    }
}