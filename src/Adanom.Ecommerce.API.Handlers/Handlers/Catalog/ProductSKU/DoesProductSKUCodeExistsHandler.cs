namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductSKUCodeExistsHandler : IRequestHandler<DoesProductSKUCodeExists, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProductSKUCodeExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory  )
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProductSKUCodeExists command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.ProductSKUs
                .AnyAsync(e => e.DeletedAtUtc == null && e.Code.ToUpper() == command.Code.ToUpper());
        }

        #endregion
    }
}
