namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartItemHandler : IRequestHandler<GetAnonymousShoppingCartItem, AnonymousShoppingCartItemResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartItemHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<AnonymousShoppingCartItemResponse?> Handle(GetAnonymousShoppingCartItem command, CancellationToken cancellationToken)
        {
            AnonymousShoppingCartItem? anonymousShoppingCartItem = null;

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (command.Id != null)
            {
                anonymousShoppingCartItem = await applicationDbContext.AnonymousShoppingCartItems
                    .Where(e => e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            else
            {
                anonymousShoppingCartItem = await applicationDbContext.AnonymousShoppingCartItems
                    .Where(e =>
                        e.AnonymousShoppingCart.Id == command.AnonymousShoppingCartId &&
                        e.ProductId == command.ProductId)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }

            return _mapper.Map<AnonymousShoppingCartItemResponse>(anonymousShoppingCartItem);
        }

        #endregion
    }
}
