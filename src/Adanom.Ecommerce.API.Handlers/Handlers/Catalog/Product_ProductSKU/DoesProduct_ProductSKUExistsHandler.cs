namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProduct_ProductSKUExistsHandler : IRequestHandler<DoesProduct_ProductSKUExists, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProduct_ProductSKUExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProduct_ProductSKUExists command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.Product_ProductSKU_Mappings
                .AnyAsync(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductSKUId == command.ProductSKUId);
        }

        #endregion
    }
}
