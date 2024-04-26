namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ChangePasswordRequest
    {
        public string Email { get; set; } = null!;

        public string OldPassword { get; set; } = null!;

        public string NewPassword { get; set; } = null!;

        public string ConfirmNewPassword { get; set; } = null!;
    }
}