namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderPaymentsHandler : IRequestHandler<GetOrderPayments, PaginatedData<OrderPaymentResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderPaymentsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<OrderPaymentResponse>> Handle(GetOrderPayments command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var orderPaymentsQuery = applicationDbContext.OrderPayments.AsNoTracking();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (command.Filter != null)
                {
                    #region Apply filtering

                    if (command.Filter.StartDate != null)
                    {
                        var startDate = command.Filter.StartDate.Value.StartOfDate();

                        orderPaymentsQuery = orderPaymentsQuery.Where(e => e.Order.CreatedAtUtc.Date >= startDate);
                    }

                    if (command.Filter.EndDate != null)
                    {
                        var endDate = command.Filter.EndDate.Value.EndOfDate();

                        orderPaymentsQuery = orderPaymentsQuery.Where(e => e.Order.CreatedAtUtc.Date <= endDate);
                    }

                    #endregion

                    #region Apply orderPaymenting

                    orderPaymentsQuery = command.Filter.OrderBy switch
                    {
                        GetOrderPaymentsOrderByEnum.CREATED_AT_ASC =>
                            orderPaymentsQuery.OrderBy(e => e.Order.CreatedAtUtc),
                        _ =>
                            orderPaymentsQuery.OrderByDescending(e => e.Order.CreatedAtUtc)
                    };

                    #endregion
                }

                #endregion
            }

            var totalCount = orderPaymentsQuery.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                orderPaymentsQuery = orderPaymentsQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var orderPaymentResponses = _mapper.Map<IEnumerable<OrderPaymentResponse>>(await orderPaymentsQuery.ToListAsync());

            return new PaginatedData<OrderPaymentResponse>(
                orderPaymentResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
