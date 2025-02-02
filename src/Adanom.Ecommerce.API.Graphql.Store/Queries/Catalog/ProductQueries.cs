namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class ProductQueries
    {
        #region GetProductByIdAsync

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [GraphQLDescription("Gets products")]
        public async Task<ProductCatalogResponse> GetProductsAsync(
            PaginationRequest paginationRequest,
            GetProductsFilter? filter,
            [Service] IMediator mediator)
        {
            if (filter != null)
            {
                filter.IsRequestFromStoreClient = true;
            }

            var command = new GetProducts(paginationRequest, filter);

            return await mediator.Send(command);
        }

        #endregion

        #region GetRelatedProductsAsync

        [AllowAnonymous]
        [GraphQLDescription("Gets related products")]
        public async Task<IEnumerable<ProductResponse>> GetRelatedProductsAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetRelatedProducts(id);

            return await mediator.Send(command);
        }

        #endregion
    }
}
