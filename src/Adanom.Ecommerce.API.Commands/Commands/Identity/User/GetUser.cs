namespace Adanom.Ecommerce.API.Commands
{
    public class GetUser : IRequest<UserResponse?>
    {
        #region Ctor

        public GetUser(Guid id, bool? includeRoles = null)
        {
            Id = id;
            IncludeRoles = includeRoles;

        }

        public GetUser(string? email, bool? includeRoles = null)
        {
            Email = email;
            IncludeRoles = includeRoles;
        }

        #endregion

        #region Properties

        public Guid Id { get; set; }

        public string? Email { get; set; }

        public bool? IncludeRoles { get; set; }

        #endregion
    }
}
