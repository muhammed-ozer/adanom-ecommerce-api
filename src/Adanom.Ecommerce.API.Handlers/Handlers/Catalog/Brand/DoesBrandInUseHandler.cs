namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesBrandInUseHandler : IRequestHandler<DoesEntityInUse<BrandResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesBrandInUseHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityInUse<BrandResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null && e.BrandId == command.Id)
                .AnyAsync();
        }

        #endregion
    }
}
