using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShippingAddressesCountHandler : IRequestHandler<GetShippingAddressesCount, long>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public GetShippingAddressesCountHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<long> Handle(GetShippingAddressesCount command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var totalCount = await _applicationDbContext.ShippingAddresses
                .Where(e => e.DeletedAtUtc == null && e.UserId == userId)
                .AsNoTracking()
                .CountAsync();

            return totalCount;
        }

        #endregion
    }
}
