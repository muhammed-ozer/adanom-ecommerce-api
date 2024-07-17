namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartItemsCountHandler : IRequestHandler<GetAnonymousShoppingCartItemsCount, int>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartItemsCountHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<int> Handle(GetAnonymousShoppingCartItemsCount command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.AnonymousShoppingCartItems
                .Where(e => e.AnonymousShoppingCart.Id == command.AnonymousSHoppingCartId)
                .CountAsync();
        }

        #endregion
    }
}
