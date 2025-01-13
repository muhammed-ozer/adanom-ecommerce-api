namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesTaxCategoryInUseHandler : IRequestHandler<DoesEntityInUse<TaxCategoryResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesTaxCategoryInUseHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityInUse<TaxCategoryResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.ProductPrices
                .Where(e => e.DeletedAtUtc == null && e.TaxCategoryId == command.Id)
                .AnyAsync();
        }

        #endregion
    }
}
