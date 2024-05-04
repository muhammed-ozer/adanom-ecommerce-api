namespace Adanom.Ecommerce.API.Commands
{
    public sealed class DoesUserExists : IRequest<bool>
    {
        public DoesUserExists(string email)
        {
            Email = email;
        }

        public DoesUserExists(Guid id)
        {
            Id = id;
        }

        public string? Email { get; set; }

        public Guid? Id { get; set; }
    }
}
