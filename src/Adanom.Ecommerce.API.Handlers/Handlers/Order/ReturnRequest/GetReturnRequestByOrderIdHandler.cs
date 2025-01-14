namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetReturnRequestByOrderIdHandler : IRequestHandler<GetReturnRequestByOrderId, ReturnRequestResponse?>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetReturnRequestByOrderIdHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ReturnRequestResponse?> Handle(GetReturnRequestByOrderId command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var returnRequest = await applicationDbContext.ReturnRequests
                .AsNoTracking()
                .Where(e => e.OrderId == command.Id)
                .SingleOrDefaultAsync();

            var returnRequestResponse = _mapper.Map<ReturnRequestResponse>(returnRequest);

            return returnRequestResponse;
        }

        #endregion
    }
}
