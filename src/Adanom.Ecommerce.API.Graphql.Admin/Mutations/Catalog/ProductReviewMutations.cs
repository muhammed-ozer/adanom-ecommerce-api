using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductReviewMutations
    {
        #region UpdateProductRewiewAsync

        [GraphQLDescription("Updates a product review")]
        public async Task<bool> UpdateProductRewiewAsync(
            UpdateProductReviewRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductReview(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
