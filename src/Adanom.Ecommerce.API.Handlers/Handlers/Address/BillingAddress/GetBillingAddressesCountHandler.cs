using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetBillingAddressesCountHandler : IRequestHandler<GetBillingAddressesCount, long>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public GetBillingAddressesCountHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<long> Handle(GetBillingAddressesCount command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var totalCount = await applicationDbContext.BillingAddresses
                .Where(e => e.DeletedAtUtc == null && e.UserId == userId)
                .AsNoTracking()
                .CountAsync();

            return totalCount;
        }

        #endregion
    }
}
