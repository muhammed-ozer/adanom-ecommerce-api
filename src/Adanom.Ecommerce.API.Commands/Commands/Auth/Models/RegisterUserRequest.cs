namespace Adanom.Ecommerce.API.Commands.Models
{
    public class RegisterUserRequest
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public bool AllowCommercialEmails { get; set; }

        public bool AllowCommercialSMS { get; set; }

        public bool AgreesMembershipAgreement { get; set; }

        public bool AgreesDataProtectionPolicy { get; set; }
    }
}