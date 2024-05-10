namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductPriceExistsHandler : IRequestHandler<DoesEntityExists<ProductPriceResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductPriceExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ProductPriceResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ProductPrices
                .AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id);
        }

        #endregion
    }
}
