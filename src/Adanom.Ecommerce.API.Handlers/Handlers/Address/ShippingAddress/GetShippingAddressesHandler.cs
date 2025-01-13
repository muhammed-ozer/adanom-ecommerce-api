using System.Collections.Concurrent;
using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShippingAddressesHandler : IRequestHandler<GetShippingAddresses, PaginatedData<ShippingAddressResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetShippingAddressesHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<ShippingAddressResponse>> Handle(GetShippingAddresses command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var shippingAddressesQuery = applicationDbContext.ShippingAddresses
                .Where(e => e.DeletedAtUtc == null && e.UserId == userId)
                .AsNoTracking();

            var totalCount = await shippingAddressesQuery.CountAsync();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                shippingAddressesQuery = shippingAddressesQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var shippingAddressResponses = _mapper.Map<IEnumerable<ShippingAddressResponse>>(await shippingAddressesQuery.ToListAsync());

            return new PaginatedData<ShippingAddressResponse>(
                shippingAddressResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
