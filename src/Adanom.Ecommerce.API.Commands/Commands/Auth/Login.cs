namespace Adanom.Ecommerce.API.Commands
{
    public sealed class Login : IRequest<UserResponse?>
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
