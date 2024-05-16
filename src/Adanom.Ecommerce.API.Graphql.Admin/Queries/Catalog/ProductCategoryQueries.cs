namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductCategoryQueries
    {
        #region GetProductCategoryByIdAsync

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
