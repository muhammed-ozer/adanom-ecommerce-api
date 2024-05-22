namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(NotificationResponse))]
    public sealed class NotificationResolvers
    {
        #region GetRedByUserAsync

        public async Task<UserResponse?> GetRedByUserAsync(
           [Parent] NotificationResponse notificationResponse,
           [Service] IMediator mediator)
        {
            if (notificationResponse.ReadByUserId != null)
            {
                var user = await mediator.Send(new GetUser(notificationResponse.ReadByUserId.Value));

                return user;
            }

            return null;
        }

        #endregion

        #region GetNotificationStatusTypeAsync

        public async Task<NotificationTypeResponse> GetNotificationTypeAsync(
           [Parent] NotificationResponse notificationResponse,
           [Service] IMediator mediator)
        {
            var notificationType = await mediator.Send(new GetNotificationType(notificationResponse.NotificationType.Key));

            return notificationType;
        }

        #endregion
    }
}
