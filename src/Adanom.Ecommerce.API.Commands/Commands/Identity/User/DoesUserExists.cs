namespace Adanom.Ecommerce.API.Commands
{
    public sealed class DoesUserExists : IRequest<bool>
    {
        #region Ctor

        public DoesUserExists(Guid id)
        {
            Id = id;
        }

        public DoesUserExists(string? email)
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
