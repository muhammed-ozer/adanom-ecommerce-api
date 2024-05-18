using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class UserMutations
    {
        #region UpdateUserDefaultDiscountRateAsync

        [GraphQLDescription("Updates an user default discount rate")]
        public async Task<bool> UpdateUserDefaultDiscountRateAsync(
            UpdateUserDefaultDiscountRateRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateUserDefaultDiscountRate(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
