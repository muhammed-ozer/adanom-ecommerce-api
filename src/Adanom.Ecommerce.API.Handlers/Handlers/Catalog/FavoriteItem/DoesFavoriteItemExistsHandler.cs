namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesFavoriteItemExistsHandler : IRequestHandler<DoesUserEntityExists<FavoriteItemResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesFavoriteItemExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesUserEntityExists<FavoriteItemResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.FavoriteItems
                .AnyAsync(e => e.UserId == command.UserId && 
                               e.Id == command.Id);
        }

        #endregion
    }
}
