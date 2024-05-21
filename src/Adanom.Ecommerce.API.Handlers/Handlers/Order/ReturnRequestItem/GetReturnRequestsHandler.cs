namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetReturnRequestItemsHandler : IRequestHandler<GetReturnRequestItems, IEnumerable<ReturnRequestItemResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetReturnRequestItemsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ReturnRequestItemResponse>> Handle(GetReturnRequestItems command, CancellationToken cancellationToken)
        {
            var returnRequestItems = await _applicationDbContext.ReturnRequestItems
                .AsNoTracking()
                .Where(e => e.ReturnRequestId == command.Filter.ReturnRequestId)
                .ToListAsync();

            var returnRequestItemResponses = _mapper.Map<IEnumerable<ReturnRequestItemResponse>>(returnRequestItems);

            return returnRequestItemResponses;
        }

        #endregion
    }
}
