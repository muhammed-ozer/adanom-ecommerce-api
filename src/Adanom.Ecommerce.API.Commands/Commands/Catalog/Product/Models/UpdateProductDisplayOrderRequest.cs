namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductDisplayOrderRequest
    {
        public long Id { get; set; }

        public int DisplayOrder { get; set; }
    }
}