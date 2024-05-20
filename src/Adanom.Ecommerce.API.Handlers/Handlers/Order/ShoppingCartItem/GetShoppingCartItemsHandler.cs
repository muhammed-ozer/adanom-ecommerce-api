namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCartItemsHandler : IRequestHandler<GetShoppingCartItems, IEnumerable<ShoppingCartItemResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetShoppingCartItemsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ShoppingCartItemResponse>> Handle(GetShoppingCartItems command, CancellationToken cancellationToken)
        {
            var shoppingCartItems = await _applicationDbContext.ShoppingCartItems
                .AsNoTracking()
                .Where(e => e.ShoppingCartId == command.Filter.ShoppingCartId)
                .ToListAsync();

            var shoppingCartItemResponses = _mapper.Map<IEnumerable<ShoppingCartItemResponse>>(shoppingCartItems);

            return shoppingCartItemResponses;
        }

        #endregion
    }
}
