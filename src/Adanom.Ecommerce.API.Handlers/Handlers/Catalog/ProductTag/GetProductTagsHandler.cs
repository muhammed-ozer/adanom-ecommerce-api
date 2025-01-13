using System.Collections.Concurrent;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductTagsHandler :
        IRequestHandler<GetProductTags, PaginatedData<ProductTagResponse>>,
        INotificationHandler<ClearEntityCache<ProductTagResponse>>,
        INotificationHandler<AddToCache<ProductTagResponse>>,
        INotificationHandler<UpdateFromCache<ProductTagResponse>>,
        INotificationHandler<RemoveFromCache<ProductTagResponse>>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, ProductTagResponse> _cache = new();

        #endregion

        #region Ctor

        public GetProductTagsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<ProductTagResponse>> Handle(GetProductTags command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productTagsCountOnDb = await applicationDbContext.ProductTags
                .Where(e => e.DeletedAtUtc == null)
                .CountAsync();

            if (!_cache.Values.Any() || productTagsCountOnDb != _cache.Count)
            {
                _cache.Clear();

                var productTagsOnDb = await applicationDbContext.ProductTags
                   .AsNoTracking()
                   .Where(e => e.DeletedAtUtc == null)
                   .ToListAsync();

                var productTagResponses = _mapper
                    .Map<IEnumerable<ProductTagResponse>>(productTagsOnDb);

                foreach (var item in productTagResponses)
                {
                    _cache.TryAdd(item.Id, item);
                }
            }

            var productTags = _cache.Values.AsEnumerable();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (!string.IsNullOrEmpty(command.Filter.Query))
                {
                    productTags = productTags
                        .Where(e => e.Value.Contains(command.Filter.Query, StringComparison.InvariantCultureIgnoreCase));
                }

                #endregion

                #region Apply ordering

                productTags = command.Filter.OrderBy switch
                {
                    GetProductTagsOrderByEnum.VALUE_DESC =>
                        productTags.OrderByDescending(e => e.Value),
                    _ =>
                        productTags.OrderBy(e => e.Value)
                };

                #endregion
            }
            else
            {
                productTags = productTags.OrderBy(e => e.Value);
            }

            var totalCount = productTags.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                productTags = productTags
                .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                .Take(command.Pagination.PageSize);
            }

            #endregion

            return new PaginatedData<ProductTagResponse>(
                productTags,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(ClearEntityCache<ProductTagResponse> command, CancellationToken cancellationToken)
        {
            _cache.Clear();

            return Task.CompletedTask;
        }

        public Task Handle(AddToCache<ProductTagResponse> command, CancellationToken cancellationToken)
        {
            var result = _cache.TryAdd(command.Entity.Id, command.Entity);

            if (!result)
            {
                _cache.Clear();
            }

            return Task.CompletedTask;
        }

        public Task Handle(UpdateFromCache<ProductTagResponse> command, CancellationToken cancellationToken)
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

        public Task Handle(RemoveFromCache<ProductTagResponse> command, CancellationToken cancellationToken)
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
