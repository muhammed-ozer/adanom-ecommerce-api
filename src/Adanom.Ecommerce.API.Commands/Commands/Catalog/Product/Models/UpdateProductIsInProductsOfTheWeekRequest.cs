namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductIsInProductsOfTheWeekRequest
    {
        public long Id { get; set; }

        public bool IsInProductsOfTheWeek { get; set; }
    }
}