namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesShoppingCartItemExistsHandler : IRequestHandler<DoesEntityExists<ShoppingCartItemResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesShoppingCartItemExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ShoppingCartItemResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.ShoppingCartItems
                .AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
