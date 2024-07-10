namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class TaxCategoryQueries
    {
        #region GetTaxCategoryAsync

        [GraphQLDescription("Gets tax category")]
        public async Task<TaxCategoryResponse?> GetTaxCategoryAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetTaxCategory(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetTaxCategoriesAsync

        [GraphQLDescription("Gets tax categories")]
        public async Task<PaginatedData<TaxCategoryResponse>> GetTaxCategoriesAsync(
            GetTaxCategoriesFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetTaxCategories(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
