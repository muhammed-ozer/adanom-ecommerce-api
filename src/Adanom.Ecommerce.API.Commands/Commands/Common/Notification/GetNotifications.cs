namespace Adanom.Ecommerce.API.Commands
{
    public class GetNotifications : IRequest<PaginatedData<NotificationResponse>>
    {
        #region Ctor

        public GetNotifications(GetNotificationsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetNotificationsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
