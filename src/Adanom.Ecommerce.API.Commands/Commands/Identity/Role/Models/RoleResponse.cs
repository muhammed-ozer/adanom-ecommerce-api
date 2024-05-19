namespace Adanom.Ecommerce.API.Commands.Models
{
    public sealed class RoleResponse : BaseEntity<Guid>
    {
        public string Name { get; set; } = null!;
    }
}
