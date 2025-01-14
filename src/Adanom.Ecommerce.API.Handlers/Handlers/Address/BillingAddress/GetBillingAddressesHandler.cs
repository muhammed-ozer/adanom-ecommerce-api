using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetBillingAddressesHandler : IRequestHandler<GetBillingAddresses, PaginatedData<BillingAddressResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetBillingAddressesHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<BillingAddressResponse>> Handle(GetBillingAddresses command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var billingAddressesQuery = applicationDbContext.BillingAddresses
                .Where(e => e.DeletedAtUtc == null && e.UserId == userId)
                .AsNoTracking();

            var totalCount = await billingAddressesQuery.CountAsync();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                billingAddressesQuery = billingAddressesQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var billingAddressResponses = _mapper.Map<IEnumerable<BillingAddressResponse>>(await billingAddressesQuery.ToListAsync());

            return new PaginatedData<BillingAddressResponse>(
                billingAddressResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
