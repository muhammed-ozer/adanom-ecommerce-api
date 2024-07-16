namespace Adanom.Ecommerce.API.Graphql.Store.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize]
    public sealed class ProductReveiwMutations
    {
        #region CreateProductReviewAsync

        [GraphQLDescription("Creates a product review")]
        public async Task<bool> CreateProductReviewAsync(
            CreateProductReviewRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateProductReview(identity));

            return await mediator.Send(command);
        }

        #endregion
    }
}
