namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class ReturnRequestStatusTypeQueries
    {
        #region GetReturnRequestStatusTypesAsync

        [GraphQLDescription("Gets return request status types")]
        public async Task<IEnumerable<ReturnRequestStatusTypeResponse>> GetReturnRequestStatusTypesAsync([Service] IMediator mediator)
        {
            var command = new GetReturnRequestStatusTypes();

            return await mediator.Send(command);
        }

        #endregion
    }
}
