namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class SliderItemQueries
    {
        #region GetSliderItemAsync

        [GraphQLDescription("Gets a slider item")]
        public async Task<SliderItemResponse?> GetSliderItemAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetSliderItem(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetSliderItemsAsync

        [GraphQLDescription("Gets slider items")]
        public async Task<PaginatedData<SliderItemResponse>> GetSliderItemsAsync(
            GetSliderItemsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetSliderItems(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
