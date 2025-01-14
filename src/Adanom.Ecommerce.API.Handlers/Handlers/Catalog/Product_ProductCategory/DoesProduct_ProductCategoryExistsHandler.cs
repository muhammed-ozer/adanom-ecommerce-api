namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProduct_ProductCategoryExistsHandler : IRequestHandler<DoesProduct_ProductCategoryExists, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProduct_ProductCategoryExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProduct_ProductCategoryExists command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.Product_ProductCategory_Mappings
                .AnyAsync(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductCategoryId == command.ProductCategoryId);
        }

        #endregion
    }
}
