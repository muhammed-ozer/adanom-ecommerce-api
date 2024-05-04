namespace Adanom.Ecommerce.API.Commands
{
    public class SendEmailConfirmationEmail : IRequest<bool>
    {
        public string Email { get; set; } = null!;
    }
}