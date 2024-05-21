using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class OrderMutations
    {
        #region UpdateOrder_OrderStatusTypeAsync

        [GraphQLDescription("Updates an orders order status type")]
        public async Task<bool> UpdateOrder_OrderStatusTypeAsync(
            UpdateOrder_OrderStatusTypeRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateOrder_OrderStatusType(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
