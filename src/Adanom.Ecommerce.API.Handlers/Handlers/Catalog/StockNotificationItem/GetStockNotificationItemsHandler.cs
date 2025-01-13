namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetStockNotificationItemsHandler : IRequestHandler<GetStockNotificationItems, PaginatedData<StockNotificationItemResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetStockNotificationItemsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<StockNotificationItemResponse>> Handle(GetStockNotificationItems command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var stockNotificationItemsQuery = applicationDbContext.StockNotificationItems.AsNoTracking();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (command.Filter.UserId != null)
                {
                    stockNotificationItemsQuery = stockNotificationItemsQuery.Where(e => e.UserId == command.Filter.UserId);
                }

                #endregion

                #region Apply ordering

                stockNotificationItemsQuery = command.Filter.OrderBy switch
                {
                    GetStockNotificationItemsOrderByEnum.CREATED_AT_ASC =>
                        stockNotificationItemsQuery.OrderBy(e => e.CreatedAtUtc),
                    _ =>
                        stockNotificationItemsQuery.OrderByDescending(e => e.CreatedAtUtc)
                };

                #endregion
            }

            var totalCount = stockNotificationItemsQuery.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                stockNotificationItemsQuery = stockNotificationItemsQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var stockNotificationItemResponses = _mapper.Map<IEnumerable<StockNotificationItemResponse>>(await stockNotificationItemsQuery.ToListAsync());

            return new PaginatedData<StockNotificationItemResponse>(
                stockNotificationItemResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
