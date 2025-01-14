namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteExpiredAnonymousShoppingCartsHandler : IRequestHandler<DeleteExpiredAnonymousShoppingCarts, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteExpiredAnonymousShoppingCartsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteExpiredAnonymousShoppingCarts command, CancellationToken cancellationToken)
        {
            var currentExpirationTimeUtc = DateTime.UtcNow.AddDays(7 * -1);

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var anonymousShoppingCarts = await applicationDbContext.AnonymousShoppingCarts
                .Where(e => e.LastModifiedAtUtc <= currentExpirationTimeUtc)
                .ToListAsync();

            applicationDbContext.AnonymousShoppingCarts.RemoveRange(anonymousShoppingCarts);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
