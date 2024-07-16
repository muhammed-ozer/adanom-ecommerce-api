namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class ProductSKUQueries
    {
        #region GetProductSKUByIdAsync

        [AllowAnonymous]
        [GraphQLDescription("Gets product SKU by id")]
        public async Task<ProductSKUResponse?> GetProductSKUByIdAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetProductSKU(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetProductSKUByCodeAsync

        [AllowAnonymous]
        [GraphQLDescription("Gets product SKU by code")]
        public async Task<ProductSKUResponse?> GetProductSKUByCodeAsync(
            string code,
            [Service] IMediator mediator)
        {
            var command = new GetProductSKU(code);

            return await mediator.Send(command);
        }

        #endregion
    }
}
