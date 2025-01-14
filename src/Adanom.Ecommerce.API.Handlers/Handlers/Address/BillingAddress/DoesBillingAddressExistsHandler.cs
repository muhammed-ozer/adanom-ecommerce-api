namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesBillingAddressExistsHandler : IRequestHandler<DoesUserEntityExists<BillingAddressResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesBillingAddressExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesUserEntityExists<BillingAddressResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.BillingAddresses
                .AnyAsync(e => e.DeletedAtUtc == null &&
                               e.UserId == command.UserId &&
                               e.Id == command.Id);
        }

        #endregion
    }
}
