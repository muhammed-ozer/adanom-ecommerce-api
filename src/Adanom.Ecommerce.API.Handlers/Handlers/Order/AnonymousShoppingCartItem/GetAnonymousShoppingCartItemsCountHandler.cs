namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartItemsCountHandler : IRequestHandler<GetAnonymousShoppingCartItemsCount, int>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartItemsCountHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<int> Handle(GetAnonymousShoppingCartItemsCount command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.AnonymousShoppingCartItems
                .Where(e => e.AnonymousShoppingCart.Id == command.AnonymousSHoppingCartId)
                .CountAsync();
        }

        #endregion
    }
}
