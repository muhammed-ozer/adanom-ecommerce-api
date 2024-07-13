namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class SliderItemQueries
    {
        #region GetSliderItemAsync

        [AllowAnonymous]
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

        [AllowAnonymous]
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
