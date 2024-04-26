using MediatR;

namespace Adanom.Ecommerce.API.Commands
{
    public class ResetPassword : IRequest<bool>
    {
        public string Email { get; set; } = null!;

        public string Token { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}