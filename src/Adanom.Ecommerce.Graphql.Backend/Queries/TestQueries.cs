using System.Security.Claims;
using Adanom.Ecommerce.Commands;
using Adanom.Ecommerce.Commands.Models;
using Adanom.Ecommerce.Graphql.Attributes;
using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using MediatR;

namespace Adanom.Ecommerce.Graphql.Backend.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class TestQueries
    {
        #region TestAsync

        [GraphQLDescription("Test")]
        public async Task<bool> GetTestAsync(TestModel model, [Service] IMediator mediator, [Identity] ClaimsPrincipal identity)
        {
            await mediator.Send(new Test());
            return true;
        }

        #endregion
    }
}
