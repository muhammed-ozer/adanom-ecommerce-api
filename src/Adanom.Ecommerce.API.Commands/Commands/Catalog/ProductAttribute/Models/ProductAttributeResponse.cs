namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductAttributeResponse : BaseResponseEntity<long>
    {
        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;
    }
}