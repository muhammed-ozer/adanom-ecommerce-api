namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductIsActiveRequest
    {
        public long Id { get; set; }

        public bool IsActive { get; set; }
    }
}