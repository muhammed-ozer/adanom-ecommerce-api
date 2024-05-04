namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class BrandQueries
    {
        #region GetBrandByIdAsync

        [GraphQLDescription("Gets brand by id")]
        public async Task<BrandResponse?> GetBrandByIdAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetBrandById(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetBrandByUrlSlugAsync

        [GraphQLDescription("Gets brand by id")]
        public async Task<BrandResponse?> GetBrandByUrlSlugAsync(
            string urlSlug,
            [Service] IMediator mediator)
        {
            var command = new GetBrandByUrlSlug(urlSlug);

            return await mediator.Send(command);
        }

        #endregion

        #region GetBrandsAsync

        [GraphQLDescription("Gets brands")]
        public async Task<PaginatedData<BrandResponse>> GetBrandsAsync(
            GetBrandsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetBrands(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
