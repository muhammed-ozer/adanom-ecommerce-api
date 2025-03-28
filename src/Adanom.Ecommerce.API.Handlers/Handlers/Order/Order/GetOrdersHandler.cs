namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrdersHandler : IRequestHandler<GetOrders, PaginatedData<OrderResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrdersHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<OrderResponse>> Handle(GetOrders command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var ordersQuery = applicationDbContext.Orders.AsNoTracking();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (command.Filter != null)
                {
                    #region Apply filtering

                    if (!string.IsNullOrEmpty(command.Filter.Query))
                    {
                        ordersQuery = ordersQuery.Where(e => e.OrderNumber.Contains(command.Filter.Query));
                    }

                    if (command.Filter.UserId != null && command.Filter.UserId != Guid.Empty)
                    {
                        ordersQuery = ordersQuery.Where(e => e.UserId == command.Filter.UserId);
                    }

                    if (command.Filter.OrderStatusType != null)
                    {
                        ordersQuery = ordersQuery.Where(e => e.OrderStatusType == command.Filter.OrderStatusType);
                    }

                    if (command.Filter.StartDate != null)
                    {
                        var startDate = command.Filter.StartDate.Value.StartOfDate();

                        ordersQuery = ordersQuery.Where(e => e.CreatedAtUtc.Date >= startDate);
                    }

                    if (command.Filter.EndDate != null)
                    {
                        var endDate = command.Filter.EndDate.Value.EndOfDate();

                        ordersQuery = ordersQuery.Where(e => e.CreatedAtUtc.Date <= endDate);
                    }

                    #endregion

                    #region Apply ordering

                    ordersQuery = command.Filter.OrderBy switch
                    {
                        GetOrdersOrderByEnum.CREATED_AT_ASC =>
                            ordersQuery.OrderBy(e => e.CreatedAtUtc),
                        GetOrdersOrderByEnum.ORDER_NUMBER_ASC =>
                            ordersQuery.OrderBy(e => e.OrderNumber),
                        GetOrdersOrderByEnum.ORDER_NUMBER_DESC =>
                            ordersQuery.OrderByDescending(e => e.OrderNumber),
                        GetOrdersOrderByEnum.GRAND_TOTAL_ASC =>
                            ordersQuery.OrderBy(e => e.GrandTotal),
                        GetOrdersOrderByEnum.GRAND_TOTAL_DESC =>
                            ordersQuery.OrderByDescending(e => e.GrandTotal),
                        _ =>
                            ordersQuery.OrderByDescending(e => e.CreatedAtUtc)
                    };

                    #endregion
                }

                #endregion
            }
            else
            {
                ordersQuery = ordersQuery.OrderByDescending(e => e.CreatedAtUtc);
            }

            var totalCount = ordersQuery.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                ordersQuery = ordersQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var orderResponses = _mapper.Map<IEnumerable<OrderResponse>>(await ordersQuery.ToListAsync());

            return new PaginatedData<OrderResponse>(
                orderResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
