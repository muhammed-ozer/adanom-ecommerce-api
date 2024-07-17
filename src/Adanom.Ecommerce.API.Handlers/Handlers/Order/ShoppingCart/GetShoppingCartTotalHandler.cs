namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCartTotalHandler : IRequestHandler<GetShoppingCartTotal, decimal>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public GetShoppingCartTotalHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<decimal> Handle(GetShoppingCartTotal command, CancellationToken cancellationToken)
        {
            var shoppingCartItemsQuery = _applicationDbContext.ShoppingCartItems.AsNoTracking();
            decimal total = 0;

            if (command.UserId != null && command.UserId != Guid.Empty)
            {
                total = await shoppingCartItemsQuery
                    .Where(e => e.ShoppingCart.UserId == command.UserId)
                    .SumAsync(e => e.Amount * e.Price);
            }
            else 
            {
                total = await shoppingCartItemsQuery
                    .Where(e => e.ShoppingCart.Id == command.Id)
                    .SumAsync(e => e.Amount * e.Price);
            }

            return total;
        }

        #endregion
    }
}
