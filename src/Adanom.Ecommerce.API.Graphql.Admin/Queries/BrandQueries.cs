namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class BrandQueries
    {
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
