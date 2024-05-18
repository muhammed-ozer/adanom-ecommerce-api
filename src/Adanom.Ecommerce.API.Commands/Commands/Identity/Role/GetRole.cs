namespace Adanom.Ecommerce.API.Commands
{
    public class GetRole : IRequest<RoleResponse?>
    {
        #region Ctor

        public GetRole(Guid id)
        {
            Id = id;
        }

        public GetRole(string? name)
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
