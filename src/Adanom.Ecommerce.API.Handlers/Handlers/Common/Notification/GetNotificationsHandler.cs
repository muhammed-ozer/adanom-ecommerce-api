namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetNotificationsHandler : IRequestHandler<GetNotifications, PaginatedData<NotificationResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetNotificationsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<NotificationResponse>> Handle(GetNotifications command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var notificationsQuery = applicationDbContext.Notifications.AsNoTracking();

            if (command.Filter != null)
            {
                #region Apply filtering

                if (!string.IsNullOrEmpty(command.Filter.Query))
                {
                    notificationsQuery = notificationsQuery.Where(e => e.Content.Contains(command.Filter.Query));
                }

                if (command.Filter.UnRead.HasValue)
                {
                    if (command.Filter.UnRead.Value)
                    {
                        notificationsQuery = notificationsQuery.Where(e => e.ReadAtUtc == null);
                    }
                    else
                    {
                        notificationsQuery = notificationsQuery.Where(e => e.ReadAtUtc != null);
                    }
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
                    GetNotificationsOrderByEnum.CREATED_AT_DESC =>
                        notificationsQuery.OrderByDescending(e => e.CreatedAtUtc),
                    GetNotificationsOrderByEnum.READ_AT_ASC =>
                        notificationsQuery.OrderBy(e => e.ReadAtUtc),
                    _ =>
                        notificationsQuery.OrderByDescending(e => e.ReadAtUtc)
                };

                #endregion
            }
            else
            {
                notificationsQuery = notificationsQuery.OrderBy(e => e.ReadAtUtc);
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
