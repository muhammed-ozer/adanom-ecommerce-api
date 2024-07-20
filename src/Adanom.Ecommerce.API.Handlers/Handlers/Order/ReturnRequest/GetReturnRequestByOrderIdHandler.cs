namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetReturnRequestByOrderIdHandler : IRequestHandler<GetReturnRequestByOrderId, ReturnRequestResponse?>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetReturnRequestByOrderIdHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ReturnRequestResponse?> Handle(GetReturnRequestByOrderId command, CancellationToken cancellationToken)
        {
            var returnRequest = await _applicationDbContext.ReturnRequests
                .AsNoTracking()
                .Where(e => e.OrderId == command.Id)
                .SingleOrDefaultAsync();

            var returnRequestResponse = _mapper.Map<ReturnRequestResponse>(returnRequest);

            return returnRequestResponse;
        }

        #endregion
    }
}
