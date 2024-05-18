namespace Adanom.Ecommerce.API.Commands
{
    public class GetUser : IRequest<UserResponse?>
    {
        #region Ctor

        public GetUser(Guid id)
        {
            Id = id;
        }

        public GetUser(string? email)
        {
            Email = email;
        }

        #endregion

        #region Properties

        public Guid Id { get; set; }

        public string? Email { get; set; }

        #endregion
    }
}
