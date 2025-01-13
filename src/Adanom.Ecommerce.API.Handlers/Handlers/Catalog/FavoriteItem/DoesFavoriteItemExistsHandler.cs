namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesFavoriteItemExistsHandler : IRequestHandler<DoesUserEntityExists<FavoriteItemResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesFavoriteItemExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesUserEntityExists<FavoriteItemResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.FavoriteItems
                .AnyAsync(e => e.UserId == command.UserId &&
                               e.Id == command.Id);
        }

        #endregion
    }
}
