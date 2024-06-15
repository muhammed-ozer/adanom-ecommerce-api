namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductSKUQueries
    {
        #region GetProductSKUByIdAsync

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
