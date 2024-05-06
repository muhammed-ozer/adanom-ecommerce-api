namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductTagResponse : BaseResponseEntity<long>
    {
        public string Value { get; set; } = null!;
    }
}