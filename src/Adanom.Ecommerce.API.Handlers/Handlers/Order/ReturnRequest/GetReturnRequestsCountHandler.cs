namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetReturnRequestsCountHandler : IRequestHandler<GetReturnRequestsCount, int>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public GetReturnRequestsCountHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<int> Handle(GetReturnRequestsCount command, CancellationToken cancellationToken)
        {
            var returnRequestsQuery = _applicationDbContext.ReturnRequests.AsNoTracking();

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
