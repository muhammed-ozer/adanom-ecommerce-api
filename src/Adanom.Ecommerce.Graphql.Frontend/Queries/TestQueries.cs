using Adanom.Ecommerce.Commands.Models;
using Adanom.Ecommerce.Commands;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace Adanom.Ecommerce.Graphql.Frontend.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class TestQueries
    {
        #region TestAsync

        [GraphQLDescription("Test")]
        public async Task<bool> GetTestAsync(TestModel model, [Service] IMediator mediator)
        {
            await mediator.Send(new Test());
            return true;
        }

        #endregion
    }
}
