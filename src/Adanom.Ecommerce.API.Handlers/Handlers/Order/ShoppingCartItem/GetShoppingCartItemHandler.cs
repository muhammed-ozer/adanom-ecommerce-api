namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCartItemHandler : IRequestHandler<GetShoppingCartItem, ShoppingCartItemResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetShoppingCartItemHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ShoppingCartItemResponse?> Handle(GetShoppingCartItem command, CancellationToken cancellationToken)
        {
            ShoppingCartItem? shoppingCartItem = null;

            if (command.Id != null)
            {
                shoppingCartItem = await _applicationDbContext.ShoppingCartItems
                    .Where(e => e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            else
            {
                shoppingCartItem = await _applicationDbContext.ShoppingCartItems
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
