namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize]
    public class ReturnRequestQueries
    {
        #region GetReturnRequestAsync

        [GraphQLDescription("Gets a return request")]
        public async Task<ReturnRequestResponse?> GetReturnRequestAsync(
            long id,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetReturnRequest(identity, id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetReturnRequestsAsync

        [GraphQLDescription("Gets return requests")]
        public async Task<PaginatedData<ReturnRequestResponse>> GetReturnRequestsAsync(
            GetReturnRequestsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetReturnRequests(filter, paginationRequest);
            var userId = identity.GetUserId();

            if (command.Filter == null)
            {
                command.Filter = new GetReturnRequestsFilter();
            }

            command.Filter.UserId = userId;

            return await mediator.Send(command);
        }

        #endregion
    }
}
