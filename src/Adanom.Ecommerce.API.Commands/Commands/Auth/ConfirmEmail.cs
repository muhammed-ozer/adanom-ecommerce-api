namespace Adanom.Ecommerce.API.Commands
{
    public class ConfirmEmail : IRequest<bool>
    {
        public string Email { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}