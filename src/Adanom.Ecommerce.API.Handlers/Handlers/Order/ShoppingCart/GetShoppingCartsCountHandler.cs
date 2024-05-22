namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCartsCountHandler : IRequestHandler<GetShoppingCartsCount, int>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public GetShoppingCartsCountHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<int> Handle(GetShoppingCartsCount command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ShoppingCarts
                .AsNoTracking()
                .CountAsync();
        }

        #endregion
    }
}
