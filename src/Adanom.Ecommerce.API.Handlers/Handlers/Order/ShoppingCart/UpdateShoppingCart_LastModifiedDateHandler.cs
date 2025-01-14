namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateShoppingCart_LastModifiedDateHandler : IRequestHandler<UpdateShoppingCart_LastModifiedDate, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateShoppingCart_LastModifiedDateHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateShoppingCart_LastModifiedDate command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var shoppingCart = await applicationDbContext.ShoppingCarts
                .Where(e => e.Id == command.Id)
                .SingleOrDefaultAsync();

            if (shoppingCart == null)
            {
                return true;
            }

            shoppingCart.LastModifiedAtUtc = DateTime.UtcNow;

            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
