using System.Collections.Concurrent;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetBrandsHandler : IRequestHandler<GetBrands, PaginatedData<BrandResponse>>,
        INotificationHandler<ClearEntityCache<BrandResponse>>,
        INotificationHandler<AddToCache<BrandResponse>>,
        INotificationHandler<UpdateFromCache<BrandResponse>>,
        INotificationHandler<RemoveFromCache<BrandResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, BrandResponse> _cache = new();

        #endregion

        #region Ctor

        public GetBrandsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<BrandResponse>> Handle(GetBrands command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var brandsCountOnDb = await applicationDbContext.Brands
                .Where(e => e.DeletedAtUtc == null)
                .CountAsync();

            if (!_cache.Values.Any() || brandsCountOnDb != _cache.Count)
            {
                _cache.Clear();

                var brandsOnDb = await applicationDbContext.Brands
                   .AsNoTracking()
                   .Where(e => e.DeletedAtUtc == null)
                   .ToListAsync();

                var brandResponses = _mapper
                    .Map<IEnumerable<BrandResponse>>(brandsOnDb);

                foreach (var item in brandResponses)
                {
                    item.Logo = await _mediator.Send(new GetEntityImage(item.Id, EntityType.BRAND, ImageType.LOGO));

                    _cache.TryAdd(item.Id, item);
                }
            }

            var brands = _cache.Values.AsEnumerable();

            if (command.Filter != null)
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
            else
            {
                brands = brands.OrderBy(e => e.DisplayOrder);
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
            _cache.Clear();

            return Task.CompletedTask;
        }

        public Task Handle(AddToCache<BrandResponse> command, CancellationToken cancellationToken)
        {
            var result = _cache.TryAdd(command.Entity.Id, command.Entity);

            if (!result)
            {
                _cache.Clear();
            }

            return Task.CompletedTask;
        }

        public Task Handle(UpdateFromCache<BrandResponse> command, CancellationToken cancellationToken)
        {
            var oldEntity = _cache.Values.SingleOrDefault(e => e.Id == command.Entity.Id);

            if (oldEntity != null)
            {
                var result = _cache.TryUpdate(command.Entity.Id, command.Entity, oldEntity);

                if (!result)
                {
                    _cache.Clear();
                }
            }

            return Task.CompletedTask;
        }

        public Task Handle(RemoveFromCache<BrandResponse> command, CancellationToken cancellationToken)
        {
            if (_cache.ContainsKey(command.Id))
            {
                var result = _cache.TryRemove(command.Id, out _);

                if (!result)
                {
                    _cache.Clear();
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
