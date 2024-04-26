namespace Adanom.Ecommerce.API.Commands.Models
{
    public sealed class UserResponse
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public List<string> Roles { get; set; } = null!;
    }
}
