namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCartTotalHandler : IRequestHandler<GetShoppingCartTotal, decimal>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public GetShoppingCartTotalHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<decimal> Handle(GetShoppingCartTotal command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var shoppingCartItemsQuery = applicationDbContext.ShoppingCartItems.AsNoTracking();
            decimal total = 0;

            if (command.UserId != null && command.UserId != Guid.Empty)
            {
                total = await shoppingCartItemsQuery
                    .Where(e => e.ShoppingCart.UserId == command.UserId)
                    .SumAsync(e => e.Amount * (e.DiscountedPrice != null ? e.DiscountedPrice.Value : e.OriginalPrice));
            }
            else
            {
                total = await shoppingCartItemsQuery
                    .Where(e => e.ShoppingCart.Id == command.Id)
                    .SumAsync(e => e.Amount * (e.DiscountedPrice != null ? e.DiscountedPrice.Value : e.OriginalPrice));
            }

            return total;
        }

        #endregion
    }
}
