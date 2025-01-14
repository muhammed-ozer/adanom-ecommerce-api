namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetReturnRequestItemsHandler : IRequestHandler<GetReturnRequestItems, IEnumerable<ReturnRequestItemResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetReturnRequestItemsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ReturnRequestItemResponse>> Handle(GetReturnRequestItems command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var returnRequestItems = await applicationDbContext.ReturnRequestItems
                .AsNoTracking()
                .Where(e => e.ReturnRequestId == command.Filter.ReturnRequestId)
                .ToListAsync();

            var returnRequestItemResponses = _mapper.Map<IEnumerable<ReturnRequestItemResponse>>(returnRequestItems);

            return returnRequestItemResponses;
        }

        #endregion
    }
}
