namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateUserDefaultDiscountRateRequest
    {
        public Guid Id { get; set; }

        public decimal DefaultDiscountRate { get; set; }
    }
}