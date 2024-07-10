namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class SliderItemTypeQueries
    {
        #region GetStockUnitTypesAsync

        [GraphQLDescription("Gets slider item types")]
        public async Task<IEnumerable<SliderItemTypeResponse>> GetSliderItemTypesAsync(
            [Service] IMediator mediator)
        {
            return await mediator.Send(new GetSliderItemTypes());
        } 

        #endregion
    }
}
