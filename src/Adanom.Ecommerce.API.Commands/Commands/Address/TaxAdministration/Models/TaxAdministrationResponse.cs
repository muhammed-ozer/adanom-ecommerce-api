namespace Adanom.Ecommerce.API.Commands.Models
{
    public class TaxAdministrationResponse : BaseResponseEntity<long>
    {
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}