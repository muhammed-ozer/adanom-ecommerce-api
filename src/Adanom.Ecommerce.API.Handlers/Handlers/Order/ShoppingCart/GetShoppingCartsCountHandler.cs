namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCartsCountHandler : IRequestHandler<GetShoppingCartsCount, int>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public GetShoppingCartsCountHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<int> Handle(GetShoppingCartsCount command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.ShoppingCarts
                .AsNoTracking()
                .CountAsync();
        }

        #endregion
    }
}
