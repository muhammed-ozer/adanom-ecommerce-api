using System.Collections.Concurrent;
using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShippingAddressesHandler : IRequestHandler<GetShippingAddresses, PaginatedData<ShippingAddressResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetShippingAddressesHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<ShippingAddressResponse>> Handle(GetShippingAddresses command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var shippingAddressesQuery = _applicationDbContext.ShippingAddresses
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
