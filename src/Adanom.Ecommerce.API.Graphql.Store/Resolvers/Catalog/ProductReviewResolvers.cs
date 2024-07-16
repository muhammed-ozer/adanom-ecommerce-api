namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
{
    [ExtendObjectType(typeof(ProductReviewResponse))]
    public sealed class ProductReviewResolvers
    {
        #region GetUserAsync

        public async Task<UserResponse?> GetUserAsync(
           [Parent] ProductReviewResponse productReviewResponse,
           [Service] IMediator mediator)
        {
            var user = await mediator.Send(new GetUser(productReviewResponse.UserId));

            return user;
        }

        #endregion
    }
}
