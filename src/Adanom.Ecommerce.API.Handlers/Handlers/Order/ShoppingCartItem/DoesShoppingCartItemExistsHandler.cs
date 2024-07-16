namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesShoppingCartItemExistsHandler : IRequestHandler<DoesEntityExists<ShoppingCartItemResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesShoppingCartItemExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ShoppingCartItemResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ShoppingCartItems
                .AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
