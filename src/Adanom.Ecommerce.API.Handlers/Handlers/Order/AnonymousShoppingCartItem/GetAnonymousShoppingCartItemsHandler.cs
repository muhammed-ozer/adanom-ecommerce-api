namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartItemsHandler : IRequestHandler<GetAnonymousShoppingCartItems, IEnumerable<AnonymousShoppingCartItemResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartItemsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<AnonymousShoppingCartItemResponse>> Handle(GetAnonymousShoppingCartItems command, CancellationToken cancellationToken)
        {
            var anonymousShoppingCartItems = await _applicationDbContext.AnonymousShoppingCartItems
                .AsNoTracking()
                .Where(e => e.AnonymousShoppingCartId == command.Filter.AnonymousShoppingCartId)
                .ToListAsync();

            var anonymousShoppingCartItemResponses = _mapper.Map<IEnumerable<AnonymousShoppingCartItemResponse>>(anonymousShoppingCartItems);

            return anonymousShoppingCartItemResponses;
        }

        #endregion
    }
}
