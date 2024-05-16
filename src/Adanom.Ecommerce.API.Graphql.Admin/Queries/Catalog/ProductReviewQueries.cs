namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductReviewQueries
    {
        #region GetProductReviewAsync

        [GraphQLDescription("Gets product review")]
        public async Task<ProductReviewResponse?> GetProductReviewAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetProductReview(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetProductReviewsAsync

        [GraphQLDescription("Gets product reviews")]
        public async Task<PaginatedData<ProductReviewResponse>> GetProductReviewsAsync(
            GetProductReviewsFilter filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetProductReviews(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
