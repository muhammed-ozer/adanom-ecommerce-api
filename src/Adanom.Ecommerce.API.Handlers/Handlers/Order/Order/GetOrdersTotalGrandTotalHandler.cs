namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrdersTotalGrandTotalHandler : IRequestHandler<GetOrdersTotalGrandTotal, decimal>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public GetOrdersTotalGrandTotalHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<decimal> Handle(GetOrdersTotalGrandTotal command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var ordersQuery = applicationDbContext.Orders.AsNoTracking();

            #region Apply filtering

            if (command.Filter != null)
            {
                if (command.Filter.OrderStatusType != null)
                {
                    ordersQuery = ordersQuery.Where(e => e.OrderStatusType == command.Filter.OrderStatusType);
                }
                else
                {
                    ordersQuery = ordersQuery.Where(e => e.OrderStatusType != OrderStatusType.CANCEL);
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
            }
            else
            {
                ordersQuery = ordersQuery.Where(e => e.OrderStatusType != OrderStatusType.CANCEL);
            }

            #endregion

            return await ordersQuery.SumAsync(e => e.GrandTotal);
        }

        #endregion
    }
}
