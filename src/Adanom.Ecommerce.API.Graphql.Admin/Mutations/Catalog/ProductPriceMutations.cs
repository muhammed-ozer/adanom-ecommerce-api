namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
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

        #region UpdateProductPrice_DiscountedPriceAsync

        [GraphQLDescription("Updates a product price discounted price")]
        public async Task<bool> UpdateProductPrice_DiscountedPriceAsync(
            UpdateProductPrice_DiscountedPriceRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductPrice_DiscountedPrice(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region BatchUpdateProductPricesAsync

        [GraphQLDescription("Updates batch product price price")]
        public async Task<bool> BatchUpdateProductPricesAsync(
            BatchUpdateProductPricesRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new BatchUpdateProductPrices(identity));

            return await mediator.Send(command);
        }

        #endregion

        #region UpdateProductPriceTaxCategoryAsync

        [GraphQLDescription("Updates a product price tax category")]
        public async Task<bool> UpdateProductPriceTaxCategoryAsync(
            UpdateProductPriceTaxCategoryRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductPriceTaxCategory(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
