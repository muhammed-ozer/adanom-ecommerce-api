namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesShippingAddressExistsHandler : IRequestHandler<DoesUserEntityExists<ShippingAddressResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesShippingAddressExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesUserEntityExists<ShippingAddressResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.ShippingAddresses
                .AnyAsync(e => e.DeletedAtUtc == null &&
                               e.UserId == command.UserId &&
                               e.Id == command.Id);
        }

        #endregion
    }
}
