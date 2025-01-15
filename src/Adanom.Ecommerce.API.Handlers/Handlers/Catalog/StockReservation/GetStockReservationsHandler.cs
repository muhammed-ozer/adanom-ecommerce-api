namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetStockReservationsHandler : IRequestHandler<GetStockReservations, IEnumerable<StockReservationResponse>>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetStockReservationsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<StockReservationResponse>> Handle(GetStockReservations command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var stockReservations = await applicationDbContext.StockReservations
                .Where(e => e.OrderId == command.OrderId)
                .AsNoTracking()
                .ToListAsync();

            var stockReservationResponses = _mapper.Map<IEnumerable<StockReservationResponse>>(stockReservations);

            return stockReservationResponses;
        }

        #endregion
    }
}
