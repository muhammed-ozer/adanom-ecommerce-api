namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrdersCountHandler : IRequestHandler<GetOrdersCount, int>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public GetOrdersCountHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<int> Handle(GetOrdersCount command, CancellationToken cancellationToken)
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
