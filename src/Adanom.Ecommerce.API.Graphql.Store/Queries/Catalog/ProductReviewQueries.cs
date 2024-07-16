namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class ProductReviewQueries
    {
        #region GetProductReviewAsync

        [AllowAnonymous]
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

        [AllowAnonymous]
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
