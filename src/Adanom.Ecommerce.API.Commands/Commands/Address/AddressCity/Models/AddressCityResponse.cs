namespace Adanom.Ecommerce.API.Commands.Models
{
    public class AddressCityResponse : BaseResponseEntity<long>
    {
        public string Name { get; set; } = null!;

        public byte Code { get; set; }
    }
}