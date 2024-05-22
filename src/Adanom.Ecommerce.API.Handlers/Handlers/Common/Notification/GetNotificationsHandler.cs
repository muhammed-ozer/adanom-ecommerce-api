namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetNotificationsHandler : IRequestHandler<GetNotifications, PaginatedData<NotificationResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetNotificationsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<NotificationResponse>> Handle(GetNotifications command, CancellationToken cancellationToken)
        {
            var notificationsQuery = _applicationDbContext.Notifications.AsNoTracking();

            if (command.Filter != null)
            {
                #region Apply filtering

                if (!string.IsNullOrEmpty(command.Filter.Query))
                {
                    notificationsQuery = notificationsQuery.Where(e => e.Content.Contains(command.Filter.Query));
                }

                if (command.Filter.NotificationType != null)
                {
                    notificationsQuery = notificationsQuery.Where(e => e.NotificationType == command.Filter.NotificationType);
                }

                if (command.Filter.StartDate != null)
                {
                    var startDate = command.Filter.StartDate.Value.StartOfDate();

                    notificationsQuery = notificationsQuery.Where(e => e.CreatedAtUtc.Date >= startDate);
                }

                if (command.Filter.EndDate != null)
                {
                    var endDate = command.Filter.EndDate.Value.EndOfDate();

                    notificationsQuery = notificationsQuery.Where(e => e.CreatedAtUtc.Date <= endDate);
                }

                #endregion

                #region Apply ordering

                notificationsQuery = command.Filter.OrderBy switch
                {
                    GetNotificationsOrderByEnum.CREATED_AT_ASC =>
                        notificationsQuery.OrderBy(e => e.CreatedAtUtc),
                    _ =>
                        notificationsQuery.OrderByDescending(e => e.CreatedAtUtc)
                };

                #endregion
            }

            var totalCount = notificationsQuery.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                notificationsQuery = notificationsQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var notificationResponses = _mapper.Map<IEnumerable<NotificationResponse>>(await notificationsQuery.ToListAsync());

            return new PaginatedData<NotificationResponse>(
                notificationResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
