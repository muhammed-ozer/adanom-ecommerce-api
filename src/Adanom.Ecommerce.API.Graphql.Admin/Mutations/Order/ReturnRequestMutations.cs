using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ReturnRequestMutations
    {
        #region UpdateReturnRequest_ReturnRequestStatusTypeAsync

        [GraphQLDescription("Updates an return requests return request status type")]
        public async Task<bool> UpdateReturnRequest_ReturnRequestStatusTypeAsync(
            UpdateReturnRequest_ReturnRequestStatusTypeRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateReturnRequest_ReturnRequestStatusType(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
