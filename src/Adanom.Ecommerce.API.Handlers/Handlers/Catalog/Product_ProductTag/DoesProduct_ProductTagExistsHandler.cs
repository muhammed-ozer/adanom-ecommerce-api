namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProduct_ProductTagExistsHandler : IRequestHandler<DoesProduct_ProductTagExists, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProduct_ProductTagExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProduct_ProductTagExists command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.Product_ProductTag_Mappings
                .AnyAsync(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductTag.Value == command.ProductTag_Value);
        }

        #endregion
    }
}
