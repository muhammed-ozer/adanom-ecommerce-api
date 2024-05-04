namespace Adanom.Ecommerce.API.Commands
{
    public class SendPasswordResetEmail : IRequest<bool>
    {
        public string Email { get; set; } = null!;
    }
}