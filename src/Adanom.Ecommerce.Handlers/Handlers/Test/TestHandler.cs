using Adanom.Ecommerce.Commands;
using MediatR;

namespace Adanom.Ecommerce.Handlers
{
    public sealed class TestHandler : IRequestHandler<Test, bool>
    {
        public Task<bool> Handle(Test command, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
