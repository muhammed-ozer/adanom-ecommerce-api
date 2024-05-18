namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreatePickUpStoreRequest
    {
        public string DisplayName { get; set; } = null!;

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        public string Address { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
    }
}