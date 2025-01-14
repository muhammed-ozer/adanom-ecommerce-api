namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductCategoryInUseHandler : IRequestHandler<DoesEntityInUse<ProductCategoryResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProductCategoryInUseHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityInUse<ProductCategoryResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.ProductCategories
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .Where(e =>
                    e.Children.Where(e => e.DeletedAtUtc == null).Any() ||
                    e.Product_ProductCategory_Mappings.Any())
                .AnyAsync();
        }

        #endregion
    }
}
