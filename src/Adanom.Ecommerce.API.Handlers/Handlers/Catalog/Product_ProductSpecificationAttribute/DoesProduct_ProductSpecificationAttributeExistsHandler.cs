namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProduct_ProductSpecificationAttributeExistsHandler : IRequestHandler<DoesProduct_ProductSpecificationAttributeExists, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProduct_ProductSpecificationAttributeExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProduct_ProductSpecificationAttributeExists command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.Product_ProductSpecificationAttribute_Mappings
                .AnyAsync(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductSpecificationAttributeId == command.ProductSpecificationAttributeId);
        }

        #endregion
    }
}
