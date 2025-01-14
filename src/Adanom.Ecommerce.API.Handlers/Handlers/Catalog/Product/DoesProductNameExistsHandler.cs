namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductNameExistsHandler : IRequestHandler<DoesEntityNameExists<ProductResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProductNameExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityNameExists<ProductResponse> command, CancellationToken cancellationToken)
        {
            var urlSlug = command.Name.ConvertToUrlSlug();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var query = applicationDbContext.Products
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null &&
                            e.UrlSlug == urlSlug);

            if (command.ExcludedEntityId != null)
            {
                query = query.Where(e => e.Id != command.ExcludedEntityId);
            }

            return await query.AnyAsync();
        }

        #endregion
    }
}
