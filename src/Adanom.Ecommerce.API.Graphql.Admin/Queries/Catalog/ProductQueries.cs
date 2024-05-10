namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType("Query")]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductQueries
    {
        #region GetProductByIdAsync

        [GraphQLDescription("Gets product by id")]
        public async Task<ProductResponse?> GetProductByIdAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetProductById(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetProductByUrlSlugAsync

        [GraphQLDescription("Gets product by url slug")]
        public async Task<ProductResponse?> GetProductByUrlSlugAsync(
            string urlSlug,
            [Service] IMediator mediator)
        {
            var command = new GetProductByUrlSlug(urlSlug);

            return await mediator.Send(command);
        }

        #endregion
    }
}
