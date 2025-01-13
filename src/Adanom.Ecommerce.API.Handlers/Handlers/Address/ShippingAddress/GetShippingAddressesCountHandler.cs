using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShippingAddressesCountHandler : IRequestHandler<GetShippingAddressesCount, long>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public GetShippingAddressesCountHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<long> Handle(GetShippingAddressesCount command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var totalCount = await applicationDbContext.ShippingAddresses
                .Where(e => e.DeletedAtUtc == null && e.UserId == userId)
                .AsNoTracking()
                .CountAsync();

            return totalCount;
        }

        #endregion
    }
}
