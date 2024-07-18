namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateUserPermissionsRequest
    {
        public bool AllowCommercialEmails { get; set; }

        public bool AllowCommercialSMS { get; set; }
    }
}