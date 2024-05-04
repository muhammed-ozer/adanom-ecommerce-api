namespace Adanom.Ecommerce.API.Commands
{
    public sealed class CanUserLogin : IRequest<bool>
    {
        public CanUserLogin(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
