namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CanUserAddProductToFavoriteItemsHandler : IRequestHandler<CanUserAddProductToFavoriteItems, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public CanUserAddProductToFavoriteItemsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CanUserAddProductToFavoriteItems command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var userHasProductOnFavoriteItems = await applicationDbContext.FavoriteItems
                .AnyAsync(e => e.UserId == command.UserId &&
                               e.ProductId == command.ProductId);

            return !userHasProductOnFavoriteItems;
        }

        #endregion
    }
}
