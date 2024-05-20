namespace Adanom.Ecommerce.API.Commands.Models
{
    public class AnonymousShoppingCartResponse : BaseResponseEntity<Guid>
    {
        public DateTime LastModifiedAtUtc { get; set; }
    }
}