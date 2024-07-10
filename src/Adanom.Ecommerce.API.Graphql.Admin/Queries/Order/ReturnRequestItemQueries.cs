namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class ReturnRequestItemQueries
    {
        #region GetReturnRequestItemsAsync

        [GraphQLDescription("Gets return request items")]
        public async Task<IEnumerable<ReturnRequestItemResponse>> GetReturnRequestItemsAsync(
            GetReturnRequestItemsFilter filter,
            [Service] IMediator mediator)
        {
            var command = new GetReturnRequestItems(filter);

            return await mediator.Send(command);
        }

        #endregion
    }
}
