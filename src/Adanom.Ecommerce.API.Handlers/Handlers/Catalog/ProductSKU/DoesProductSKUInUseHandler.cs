namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductSKUInUseHandler : IRequestHandler<DoesEntityInUse<ProductSKUResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProductSKUInUseHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityInUse<ProductSKUResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.OrderItems
                .Where(e => e.Product.ProductSKUId == command.Id)
                .AnyAsync();
        }

        #endregion
    }
}
