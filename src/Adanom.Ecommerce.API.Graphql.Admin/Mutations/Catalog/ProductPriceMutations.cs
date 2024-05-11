using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType("Mutation")]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductPriceMutations
    {
        #region UpdateProductPrice_PriceAsync

        [GraphQLDescription("Updates a product price price")]
        public async Task<bool> UpdateProductPrice_PriceAsync(
            UpdateProductPrice_PriceRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
         {
            var command = mapper.Map(request, new UpdateProductPrice_Price(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
