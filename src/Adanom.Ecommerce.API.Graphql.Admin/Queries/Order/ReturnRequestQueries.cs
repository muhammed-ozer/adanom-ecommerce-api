namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class ReturnRequestQueries
    {
        #region GetReturnRequestAsync

        [GraphQLDescription("Gets a return request")]
        public async Task<ReturnRequestResponse?> GetReturnRequestAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetReturnRequest(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetReturnRequestsAsync

        [GraphQLDescription("Gets return requests")]
        public async Task<PaginatedData<ReturnRequestResponse>> GetReturnRequestsAsync(
            GetReturnRequestsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetReturnRequests(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion

        #region GetReturnRequestsCountAsync

        [GraphQLDescription("Gets return requests count")]
        public async Task<int> GetReturnRequestsCountAsync(
            GetReturnRequestsCountFilter? filter,
            [Service] IMediator mediator)
        {
            var command = new GetReturnRequestsCount(filter);

            return await mediator.Send(command);
        }

        #endregion
    }
}
