using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetBillingAddressesCountHandler : IRequestHandler<GetBillingAddressesCount, long>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public GetBillingAddressesCountHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<long> Handle(GetBillingAddressesCount command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var totalCount = await _applicationDbContext.BillingAddresses
                .Where(e => e.DeletedAtUtc == null && e.UserId == userId)
                .AsNoTracking()
                .CountAsync();

            return totalCount;
        }

        #endregion
    }
}
