namespace Adanom.Ecommerce.API.Commands
{
    public class GetNotificationType : IRequest<NotificationTypeResponse>
    {
        #region Ctor

        public GetNotificationType(NotificationType stockUnitType)
        {
            NotificationType = stockUnitType;
        }

        #endregion

        #region Properties

        public NotificationType NotificationType { get; set; }

        #endregion
    }
}
