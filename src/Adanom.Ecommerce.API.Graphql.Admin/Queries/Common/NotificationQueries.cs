namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class NotificationQueries
    {
        #region GetNotificationsAsync

        [GraphQLDescription("Gets notifications")]
        public async Task<PaginatedData<NotificationResponse>> GetNotificationsAsync(
            GetNotificationsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetNotifications(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
