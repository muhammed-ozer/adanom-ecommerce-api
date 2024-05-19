namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateUserRolesRequest
    {
        public UpdateUserRolesRequest()
        {
            Roles = new List<string>();
        }

        public Guid Id { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}