namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrdersCountHandler : IRequestHandler<GetOrdersCount, int>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public GetOrdersCountHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<int> Handle(GetOrdersCount command, CancellationToken cancellationToken)
        {
            var ordersQuery = _applicationDbContext.Orders.AsNoTracking();

            #region Apply filtering

            if (command.Filter != null)
            {
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
            }

            #endregion

            return await ordersQuery.CountAsync();
        }

        #endregion
    }
}
