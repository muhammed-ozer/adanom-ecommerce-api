using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class TaxCategoryMutations
    {
        #region CreateTaxCategoryAsync

        [GraphQLDescription("Creates a tax category")]
        public async Task<TaxCategoryResponse?> CreateTaxCategoryAsync(
            CreateTaxCategoryRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateTaxCategory(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateTaxCategoryAsync

        [GraphQLDescription("Updates a tax category")]
        public async Task<TaxCategoryResponse?> UpdateTaxCategoryAsync(
            UpdateTaxCategoryRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateTaxCategory(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteTaxCategoryAsync

        [GraphQLDescription("Deletes a tax category")]
        public async Task<bool> DeleteTaxCategoryAsync(
            DeleteTaxCategoryRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteTaxCategory(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region ClearTaxCategoriesCacheAsync

        [GraphQLDescription("Clears tax category cache")]
        public async Task<bool> ClearTaxCategoriesCacheAsync([Service] IMediator mediator)
        {
            await mediator.Publish(new ClearEntityCache<TaxCategoryResponse>());

            return true;
        }

        #endregion
    }
}
