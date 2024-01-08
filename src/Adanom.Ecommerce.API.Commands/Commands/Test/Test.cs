using MediatR;

namespace Adanom.Ecommerce.API.Commands
{
    public sealed class Test : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
