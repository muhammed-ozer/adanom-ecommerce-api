namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrdersTotalGrandTotalHandler : IRequestHandler<GetOrdersTotalGrandTotal, decimal>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public GetOrdersTotalGrandTotalHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<decimal> Handle(GetOrdersTotalGrandTotal command, CancellationToken cancellationToken)
        {
            var ordersQuery = _applicationDbContext.Orders.AsNoTracking();

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
