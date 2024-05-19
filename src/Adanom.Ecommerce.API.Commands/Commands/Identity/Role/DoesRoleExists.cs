namespace Adanom.Ecommerce.API.Commands
{
    public sealed class DoesRoleExists : IRequest<bool>
    {
        #region Ctor

        public DoesRoleExists(Guid id)
        {
            Id = id;
        }

        public DoesRoleExists(string? name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        public Guid Id { get; set; }

        public string? Name { get; set; }

        #endregion
    }
}
