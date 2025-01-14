namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartItemsHandler : IRequestHandler<GetAnonymousShoppingCartItems, IEnumerable<AnonymousShoppingCartItemResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartItemsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<AnonymousShoppingCartItemResponse>> Handle(GetAnonymousShoppingCartItems command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var anonymousShoppingCartItems = await applicationDbContext.AnonymousShoppingCartItems
                .AsNoTracking()
                .Where(e => e.AnonymousShoppingCartId == command.Filter.AnonymousShoppingCartId)
                .ToListAsync();

            var anonymousShoppingCartItemResponses = _mapper.Map<IEnumerable<AnonymousShoppingCartItemResponse>>(anonymousShoppingCartItems);

            return anonymousShoppingCartItemResponses;
        }

        #endregion
    }
}
