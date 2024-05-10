namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductBrandRequest
    {
        public long Id { get; set; }

        public long? BrandId { get; set; }
    }
}