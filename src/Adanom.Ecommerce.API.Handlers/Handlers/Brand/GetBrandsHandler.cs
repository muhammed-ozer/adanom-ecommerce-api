using System.Collections.Concurrent;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetBrandsHandler : IRequestHandler<GetBrands, PaginatedData<BrandResponse>>, INotificationHandler<ClearEntityCache<BrandResponse>>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, BrandResponse> _cache = new();

        #endregion

        #region Ctor

        public GetBrandsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<BrandResponse>> Handle(GetBrands command, CancellationToken cancellationToken)
        {
            if (!_cache.Values.Any())
            {
                var brandsOnDb = await _applicationDbContext.Brands
                   .AsNoTracking()
                   .Where(e => e.DeletedAtUtc == null)
                   .ToListAsync();

                var brandResponses = _mapper
                    .Map<IEnumerable<BrandResponse>>(brandsOnDb);

                foreach (var item in brandResponses)
                {
                    _cache.TryAdd(item.Id, item);
                }
            }

            var brands = _cache.Values.AsEnumerable();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (!string.IsNullOrEmpty(command.Filter.Query))
                {
                    brands = brands.Where(e => e.Name.Contains(command.Filter.Query, StringComparison.InvariantCultureIgnoreCase));
                }

                #endregion

                #region Apply ordering

                brands = command.Filter.OrderBy switch
                {
                    GetBrandsOrderByEnum.DISPLAY_ORDER_DESC =>
                        brands.OrderByDescending(e => e.DisplayOrder),
                    GetBrandsOrderByEnum.NAME_ASC =>
                        brands.OrderBy(e => e.Name),
                    GetBrandsOrderByEnum.NAME_DESC =>
                        brands.OrderByDescending(e => e.Name),
                    _ =>
                        brands.OrderBy(e => e.DisplayOrder)
                };

                #endregion
            }

            var totalCount = brands.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                brands = brands
                .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                .Take(command.Pagination.PageSize);
            }

            #endregion

            return new PaginatedData<BrandResponse>(
                brands,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(ClearEntityCache<BrandResponse> command, CancellationToken cancellationToken)
        {
            if (command.Id != null)
            {
                var entity = _cache.Values.SingleOrDefault(e => e.Id == command.Id);

                if (entity != null)
                {
                    _cache.Remove(entity.Id, out entity);
                }
            }
            else
            {
                _cache.Clear();
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
