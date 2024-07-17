namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartItemHandler : IRequestHandler<GetAnonymousShoppingCartItem, AnonymousShoppingCartItemResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartItemHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<AnonymousShoppingCartItemResponse?> Handle(GetAnonymousShoppingCartItem command, CancellationToken cancellationToken)
        {
            AnonymousShoppingCartItem? anonymousShoppingCartItem = null;

            if (command.Id != null)
            {
                anonymousShoppingCartItem = await _applicationDbContext.AnonymousShoppingCartItems
                    .Where(e => e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            else
            {
                anonymousShoppingCartItem = await _applicationDbContext.AnonymousShoppingCartItems
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
