using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetReturnRequestHandler : IRequestHandler<GetReturnRequest, ReturnRequestResponse?>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetReturnRequestHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ReturnRequestResponse?> Handle(GetReturnRequest command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var returnRequestsQuery = applicationDbContext.ReturnRequests.AsNoTracking();

            if (command.Identity != null)
            {
                var userId = command.Identity.GetUserId();

                returnRequestsQuery = returnRequestsQuery.Where(e => e.Order.UserId == userId);
            }

            ReturnRequest? returnRequest;

            if (command.ReturnRequestNumber.IsNotNullOrEmpty())
            {
                returnRequest = await returnRequestsQuery.Where(e => e.ReturnRequestNumber == command.ReturnRequestNumber)
                                         .SingleOrDefaultAsync();
            }
            else
            {
                returnRequest = await returnRequestsQuery.Where(e => e.Id == command.Id)
                                         .SingleOrDefaultAsync();
            }

            var returnRequestResponse = _mapper.Map<ReturnRequestResponse>(returnRequest);

            return returnRequestResponse;
        }

        #endregion
    }
}
