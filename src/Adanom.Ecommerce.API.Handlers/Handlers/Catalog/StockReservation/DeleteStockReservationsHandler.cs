namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteStockReservationsHandler : IRequestHandler<DeleteStockReservations, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public DeleteStockReservationsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteStockReservations command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var stockReservations = await applicationDbContext.StockReservations
                .Where(e => e.OrderId == command.OrderId)
                .ToListAsync(cancellationToken);

            if (!stockReservations.Any())
            {
                return true;
            }

            if (command.DecreaseProductStockQuantity.HasValue && command.DecreaseProductStockQuantity.Value)
            {
                foreach (var stockReservation in stockReservations)
                {
                    var productSKU = await applicationDbContext.Product_ProductSKU_Mappings
                        .Where(e => e.Product.Id == stockReservation.ProductId && e.Product.DeletedAtUtc == null)
                        .Select(e => e.ProductSKU)
                        .SingleOrDefaultAsync(cancellationToken);

                    if (productSKU == null)
                    {
                        continue;
                    }

                    productSKU.StockQuantity -= stockReservation.Amount;
                }
            }

            applicationDbContext.RemoveRange(stockReservations);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
