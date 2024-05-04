namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateTaxCategoryRequest
    {
        public string Name { get; set; } = null!;

        public string GroupName { get; set; } = null!;

        public byte Rate { get; set; }
    }
}