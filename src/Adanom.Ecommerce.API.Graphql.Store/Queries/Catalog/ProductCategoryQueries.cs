namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class ProductCategoryQueries
    {
        #region GetProductCategoryByIdAsync

        [AllowAnonymous]
        [GraphQLDescription("Gets product category by id")]
        public async Task<ProductCategoryResponse?> GetProductCategoryByIdAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetProductCategory(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetProductCategoryByUrlSlugAsync

        [AllowAnonymous]
        [GraphQLDescription("Gets product category by url slug")]
        public async Task<ProductCategoryResponse?> GetProductCategoryByUrlSlugAsync(
            string urlSlug,
            [Service] IMediator mediator)
        {
            var command = new GetProductCategory(urlSlug);

            return await mediator.Send(command);
        }

        #endregion

        #region GetProductCategoriesAsync

        [AllowAnonymous]
        [GraphQLDescription("Gets product categories")]
        public async Task<PaginatedData<ProductCategoryResponse>> GetProductCategoriesAsync(
            GetProductCategoriesFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetProductCategories(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
