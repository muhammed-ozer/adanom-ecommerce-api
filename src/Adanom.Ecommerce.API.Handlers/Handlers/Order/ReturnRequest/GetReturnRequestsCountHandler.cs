namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetReturnRequestsCountHandler : IRequestHandler<GetReturnRequestsCount, int>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public GetReturnRequestsCountHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<int> Handle(GetReturnRequestsCount command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var returnRequestsQuery = applicationDbContext.ReturnRequests.AsNoTracking();

            #region Apply filtering

            if (command.Filter != null)
            {
                if (command.Filter.ReturnRequestStatusType != null)
                {
                    returnRequestsQuery = returnRequestsQuery.Where(e => e.ReturnRequestStatusType == command.Filter.ReturnRequestStatusType);
                }

                if (command.Filter.StartDate != null)
                {
                    var startDate = command.Filter.StartDate.Value.StartOfDate();

                    returnRequestsQuery = returnRequestsQuery.Where(e => e.CreatedAtUtc.Date >= startDate);
                }

                if (command.Filter.EndDate != null)
                {
                    var endDate = command.Filter.EndDate.Value.EndOfDate();

                    returnRequestsQuery = returnRequestsQuery.Where(e => e.CreatedAtUtc.Date <= endDate);
                }
            }

            #endregion

            return await returnRequestsQuery.CountAsync();
        }

        #endregion
    }
}
