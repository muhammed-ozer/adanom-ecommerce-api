using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class NotificationMutations
    {
        #region MarkAsReadNotificationsAsync

        [GraphQLDescription("Marks as read notifications")]
        public async Task<bool> MarkAsReadNotificationsAsync(
            MarkAsReadNotificationsRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new MarkAsReadNotifications(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
