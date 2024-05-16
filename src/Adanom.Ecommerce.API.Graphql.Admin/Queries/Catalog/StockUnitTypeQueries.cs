namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class StockUnitTypeQueries
    {
        #region GetStockUnitTypesAsync

        [GraphQLDescription("Gets stock unit types")]
        public async Task<IEnumerable<StockUnitTypeResponse>> GetStockUnitTypesAsync(
            [Service] IMediator mediator)
        {
            return await mediator.Send(new GetStockUnitTypes());
        } 

        #endregion
    }
}
