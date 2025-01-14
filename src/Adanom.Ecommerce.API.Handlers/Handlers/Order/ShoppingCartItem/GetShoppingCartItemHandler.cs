namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCartItemHandler : IRequestHandler<GetShoppingCartItem, ShoppingCartItemResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetShoppingCartItemHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ShoppingCartItemResponse?> Handle(GetShoppingCartItem command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            ShoppingCartItem? shoppingCartItem = null;

            if (command.Id != null)
            {
                shoppingCartItem = await applicationDbContext.ShoppingCartItems
                    .Where(e => e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            else
            {
                shoppingCartItem = await applicationDbContext.ShoppingCartItems
                    .Where(e =>
                        e.ShoppingCart.UserId == command.UserId &&
                        e.ProductId == command.ProductId)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }

            return _mapper.Map<ShoppingCartItemResponse>(shoppingCartItem);
        }

        #endregion
    }
}
