namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartHandler : IRequestHandler<GetAnonymousShoppingCart, AnonymousShoppingCartResponse?>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<AnonymousShoppingCartResponse?> Handle(GetAnonymousShoppingCart command, CancellationToken cancellationToken)
        {
            var anonymousShoppingCart = await _applicationDbContext.AnonymousShoppingCarts
                .AsNoTracking()
                .Where(e => e.Id == command.Id)
                .SingleOrDefaultAsync();

            var anonymousShoppingCartResponse = _mapper.Map<AnonymousShoppingCartResponse>(anonymousShoppingCart);

            return anonymousShoppingCartResponse;
        }

        #endregion
    }
}
