namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class ProductTagQueries
    {
        #region GetProductTagAsync

        [AllowAnonymous]
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

        [AllowAnonymous]
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
