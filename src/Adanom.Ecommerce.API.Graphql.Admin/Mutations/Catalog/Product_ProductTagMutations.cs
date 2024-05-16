using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class Product_ProductTagMutations
    {
        #region CreateProduct_ProductTagAsync

        [GraphQLDescription("Creates a product productTag")]
        public async Task<bool> CreateProduct_ProductTagAsync(
            CreateProduct_ProductTagRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateProduct_ProductTag(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteProduct_ProductTagAsync

        [GraphQLDescription("Deletes a product productTag")]
        public async Task<bool> DeleteProduct_ProductTagAsync(
            DeleteProduct_ProductTagRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteProduct_ProductTag(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
