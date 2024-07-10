namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductTagQueries
    {
        #region GetProductTagAsync

        [GraphQLDescription("Gets product tag")]
        public async Task<ProductTagResponse?> GetProductTagAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetProductTag(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetProductTagsAsync

        [GraphQLDescription("Gets product tags")]
        public async Task<PaginatedData<ProductTagResponse>> GetProductTagsAsync(
            GetProductTagsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetProductTags(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
