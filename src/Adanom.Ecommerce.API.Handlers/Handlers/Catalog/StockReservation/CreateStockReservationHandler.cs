namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateStockReservationHandler : IRequestHandler<CreateStockReservation, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateStockReservationHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateStockReservation command, CancellationToken cancellationToken)
        {
            var stockReservation = _mapper.Map<CreateStockReservation, StockReservation>(command);

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(stockReservation);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
