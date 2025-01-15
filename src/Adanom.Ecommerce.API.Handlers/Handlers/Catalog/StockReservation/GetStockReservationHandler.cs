namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetStockReservationHandler : IRequestHandler<GetStockReservation, StockReservationResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetStockReservationHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<StockReservationResponse?> Handle(GetStockReservation command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var stockReservations = await applicationDbContext.StockReservations
                .Where(e => e.Id == command.Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            var stockReservationResponse = _mapper.Map<StockReservationResponse>(stockReservations);

            return stockReservationResponse;
        } 

        #endregion
    }
}
