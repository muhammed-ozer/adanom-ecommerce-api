namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCartItemsHandler : IRequestHandler<GetShoppingCartItems, IEnumerable<ShoppingCartItemResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetShoppingCartItemsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ShoppingCartItemResponse>> Handle(GetShoppingCartItems command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var shoppingCartItems = await applicationDbContext.ShoppingCartItems
                .AsNoTracking()
                .Where(e => e.ShoppingCartId == command.Filter.ShoppingCartId)
                .ToListAsync();

            var shoppingCartItemResponses = _mapper.Map<IEnumerable<ShoppingCartItemResponse>>(shoppingCartItems);

            return shoppingCartItemResponses;
        }

        #endregion
    }
}
