using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductTagMutations
    {
        #region CreateProductTagAsync

        [GraphQLDescription("Creates a product tag")]
        public async Task<ProductTagResponse?> CreateProductTagAsync(
            CreateProductTagRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateProductTag(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductTagAsync

        [GraphQLDescription("Updates a product tag")]
        public async Task<ProductTagResponse?> UpdateProductTagAsync(
            UpdateProductTagRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductTag(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteProductTagAsync

        [GraphQLDescription("Deletes a product tag")]
        public async Task<bool> DeleteProductTagAsync(
            DeleteProductTagRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteProductTag(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region ClearProductTagsCacheAsync

        [GraphQLDescription("Clears product tag cache")]
        public async Task<bool> ClearProductTagsCacheAsync([Service] IMediator mediator)
        {
            await mediator.Publish(new ClearEntityCache<ProductTagResponse>());

            return true;
        }

        #endregion
    }
}
