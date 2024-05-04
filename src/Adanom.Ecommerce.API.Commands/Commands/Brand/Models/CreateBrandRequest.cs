namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateBrandRequest
    {
        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}