namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesBillingAddressTitleExistsHandler : IRequestHandler<DoesUserEntityNameExists<BillingAddressResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesBillingAddressTitleExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesUserEntityNameExists<BillingAddressResponse> command, CancellationToken cancellationToken)
        {
            var title = command.Name.ToLower();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var query = applicationDbContext.BillingAddresses
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null &&
                            e.UserId == command.UserId &&
                            e.Title == title);

            if (command.ExcludedEntityId != null)
            {
                query = query.Where(e => e.Id != command.ExcludedEntityId);
            }

            return await query.AnyAsync();
        }

        #endregion
    }
}
