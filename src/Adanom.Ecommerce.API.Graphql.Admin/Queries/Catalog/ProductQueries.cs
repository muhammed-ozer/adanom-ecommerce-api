namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductQueries
    {
        #region GetProductByIdAsync

        [GraphQLDescription("Gets product by id")]
        public async Task<ProductResponse?> GetProductByIdAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetProduct(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetProductByUrlSlugAsync

        [GraphQLDescription("Gets product by url slug")]
        public async Task<ProductResponse?> GetProductByUrlSlugAsync(
            string urlSlug,
            [Service] IMediator mediator)
        {
            var command = new GetProduct(urlSlug);

            return await mediator.Send(command);
        }

        #endregion

        #region GetProductsAsync

        [GraphQLDescription("Gets products")]
        public async Task<PaginatedData<ProductResponse>> GetProductsAsync(
            PaginationRequest paginationRequest,
            GetProductsFilter? filter,
            [Service] IMediator mediator)
        {
            var command = new GetProducts(paginationRequest, filter);

            return await mediator.Send(command);
        }

        #endregion

    }
}
