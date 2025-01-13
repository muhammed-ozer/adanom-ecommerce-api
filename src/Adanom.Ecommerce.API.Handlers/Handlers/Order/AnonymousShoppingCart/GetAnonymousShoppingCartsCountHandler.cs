namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAnonymousShoppingCartsCountHandler : IRequestHandler<GetAnonymousShoppingCartsCount, int>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public GetAnonymousShoppingCartsCountHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<int> Handle(GetAnonymousShoppingCartsCount command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.AnonymousShoppingCarts
                .AsNoTracking()
                .CountAsync();
        }

        #endregion
    }
}
