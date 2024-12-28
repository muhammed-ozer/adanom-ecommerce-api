using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetReturnRequestHandler : IRequestHandler<GetReturnRequest, ReturnRequestResponse?>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetReturnRequestHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ReturnRequestResponse?> Handle(GetReturnRequest command, CancellationToken cancellationToken)
        {
            var returnRequestsQuery = _applicationDbContext.ReturnRequests.AsNoTracking();

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
