using MediatR;

namespace Adanom.Ecommerce.Commands
{
    public sealed class Test : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
