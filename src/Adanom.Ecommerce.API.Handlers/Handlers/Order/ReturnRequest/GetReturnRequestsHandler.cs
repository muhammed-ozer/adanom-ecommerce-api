namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetReturnRequestsHandler : IRequestHandler<GetReturnRequests, PaginatedData<ReturnRequestResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetReturnRequestsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<ReturnRequestResponse>> Handle(GetReturnRequests command, CancellationToken cancellationToken)
        {
            var returnRequestsQuery = _applicationDbContext.ReturnRequests.AsNoTracking();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (command.Filter != null)
                {
                    #region Apply filtering

                    if (!string.IsNullOrEmpty(command.Filter.Query))
                    {
                        returnRequestsQuery = returnRequestsQuery.Where(e => e.ReturnRequestNumber.Contains(command.Filter.Query));
                    }

                    if (command.Filter.UserId != null && command.Filter.UserId != Guid.Empty)
                    {
                        returnRequestsQuery = returnRequestsQuery.Where(e => e.Order.UserId == command.Filter.UserId);
                    }

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

                    #endregion

                    #region Apply returnRequesting

                    returnRequestsQuery = command.Filter.OrderBy switch
                    {
                        GetReturnRequestsOrderByEnum.CREATED_AT_ASC =>
                            returnRequestsQuery.OrderBy(e => e.CreatedAtUtc),
                        GetReturnRequestsOrderByEnum.RETURN_REQUEST_NUMBER_ASC =>
                            returnRequestsQuery.OrderBy(e => e.ReturnRequestNumber),
                        GetReturnRequestsOrderByEnum.RETURN_REQUEST_NUMBER_DESC =>
                            returnRequestsQuery.OrderByDescending(e => e.ReturnRequestNumber),
                        _ =>
                            returnRequestsQuery.OrderByDescending(e => e.CreatedAtUtc)
                    };

                    #endregion
                }

                #endregion
            }
            else
            {
                returnRequestsQuery = returnRequestsQuery.OrderByDescending(e => e.CreatedAtUtc);
            }

            var totalCount = returnRequestsQuery.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                returnRequestsQuery = returnRequestsQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var returnRequestResponses = _mapper.Map<IEnumerable<ReturnRequestResponse>>(await returnRequestsQuery.ToListAsync());

            return new PaginatedData<ReturnRequestResponse>(
                returnRequestResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
