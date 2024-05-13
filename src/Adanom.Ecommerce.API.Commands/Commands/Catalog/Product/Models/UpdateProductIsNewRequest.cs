namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductIsNewRequest
    {
        public long Id { get; set; }

        public bool IsNew { get; set; }
    }
}