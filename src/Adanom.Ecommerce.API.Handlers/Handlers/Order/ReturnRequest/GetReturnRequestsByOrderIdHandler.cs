namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetReturnRequestsByOrderIdHandler : IRequestHandler<GetReturnRequestsByOrderId, IEnumerable<ReturnRequestResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetReturnRequestsByOrderIdHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ReturnRequestResponse>> Handle(GetReturnRequestsByOrderId command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var returnRequest = await applicationDbContext.ReturnRequests
                .AsNoTracking()
                .Where(e => e.OrderId == command.Id)
                .ToListAsync();

            var returnRequestResponses = _mapper.Map<IEnumerable<ReturnRequestResponse>>(returnRequest);

            return returnRequestResponses;
        }

        #endregion
    }
}
