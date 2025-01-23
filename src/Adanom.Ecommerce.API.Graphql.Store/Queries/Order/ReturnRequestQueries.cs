namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize]
    public class ReturnRequestQueries
    {
        #region GetReturnRequestByIdAsync

        [GraphQLDescription("Gets a return request by id")]
        public async Task<ReturnRequestResponse?> GetReturnRequestByIdAsync(
            long id,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetReturnRequest(identity, id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetReturnRequestByReturnRequestNumberAsync

        [GraphQLDescription("Gets a return request by return request number")]
        public async Task<ReturnRequestResponse?> GetReturnRequestByReturnRequestNumberAsync(
            string returnRequestNumber,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetReturnRequest(identity, returnRequestNumber);

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
