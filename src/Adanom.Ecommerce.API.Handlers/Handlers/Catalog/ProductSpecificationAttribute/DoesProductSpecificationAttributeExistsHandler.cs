namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductSpecificationAttributeExistsHandler : IRequestHandler<DoesEntityExists<ProductSpecificationAttributeResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProductSpecificationAttributeExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ProductSpecificationAttributeResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.ProductSpecificationAttributes
                .AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id);
        }

        #endregion
    }
}
