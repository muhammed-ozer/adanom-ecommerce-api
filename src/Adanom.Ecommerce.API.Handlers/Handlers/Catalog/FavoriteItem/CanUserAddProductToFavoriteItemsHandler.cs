namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CanUserAddProductToFavoriteItemsHandler : IRequestHandler<CanUserAddProductToFavoriteItems, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public CanUserAddProductToFavoriteItemsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CanUserAddProductToFavoriteItems command, CancellationToken cancellationToken)
        {
            var userHasProductOnFavoriteItems = await _applicationDbContext.FavoriteItems
                .AnyAsync(e => e.UserId == command.UserId &&
                               e.ProductId == command.ProductId);

            return !userHasProductOnFavoriteItems;
         }

        #endregion
    }
}
