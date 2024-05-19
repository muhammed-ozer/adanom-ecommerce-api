namespace Adanom.Ecommerce.API.Commands
{
    public sealed class Login : IRequest<bool>
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
