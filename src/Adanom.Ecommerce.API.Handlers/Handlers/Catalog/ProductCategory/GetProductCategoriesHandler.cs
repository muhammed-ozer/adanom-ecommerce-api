using System.Collections.Concurrent;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductCategoriesHandler : 
        IRequestHandler<GetProductCategories, PaginatedData<ProductCategoryResponse>>,
        INotificationHandler<ClearEntityCache<ProductCategoryResponse>>,
        INotificationHandler<AddToCache<ProductCategoryResponse>>,
        INotificationHandler<UpdateFromCache<ProductCategoryResponse>>,
        INotificationHandler<RemoveFromCache<ProductCategoryResponse>>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, ProductCategoryResponse> _cache = new();

        #endregion

        #region Ctor

        public GetProductCategoriesHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<ProductCategoryResponse>> Handle(GetProductCategories command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productCategoriesCountOnDb = await applicationDbContext.ProductCategories
                .Where(e => e.DeletedAtUtc == null)
                .CountAsync();

            if (!_cache.Values.Any() || productCategoriesCountOnDb != _cache.Count)
            {
                _cache.Clear();

                var productCategoriesOnDb = await applicationDbContext.ProductCategories
                   .AsNoTracking()
                   .Where(e => e.DeletedAtUtc == null)
                   .ToListAsync();

                var productCategoryResponses = _mapper
                    .Map<IEnumerable<ProductCategoryResponse>>(productCategoriesOnDb);

                foreach (var item in productCategoryResponses)
                {
                    _cache.TryAdd(item.Id, item);
                }
            }

            var productCategories = _cache.Values.AsEnumerable();

            if (command.Filter != null)
            {
                #region Apply filtering

                if (command.Filter.FirstLevelCategories != null && command.Filter.FirstLevelCategories.Value)
                {
                    productCategories = productCategories.Where(e => e.ParentId == null);
                }

                if (command.Filter.ParentId != null)
                {
                    productCategories = productCategories.Where(e => e.ParentId == command.Filter.ParentId);
                }

                if (command.Filter.Query.IsNotNullOrEmpty())
                {
                    productCategories = productCategories.Where(e => e.Name.Contains(command.Filter.Query, StringComparison.InvariantCultureIgnoreCase));
                }

                #endregion

                #region Apply ordering

                productCategories = command.Filter.OrderBy switch
                {
                    GetProductCategoriesOrderByEnum.DISPLAY_ORDER_DESC =>
                        productCategories.OrderByDescending(e => e.DisplayOrder),
                    GetProductCategoriesOrderByEnum.NAME_ASC =>
                        productCategories.OrderBy(e => e.Name),
                    GetProductCategoriesOrderByEnum.NAME_DESC =>
                        productCategories.OrderByDescending(e => e.Name),
                    _ =>
                        productCategories.OrderBy(e => e.DisplayOrder)
                };

                #endregion
            }
            else
            {
                productCategories = productCategories.OrderBy(e => e.DisplayOrder);
            }

            var totalCount = productCategories.Count();

            #region Apply pagination

            if (command.Pagination != null)
            {
                productCategories = productCategories
                .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                .Take(command.Pagination.PageSize);
            }

            #endregion

            return new PaginatedData<ProductCategoryResponse>(
                productCategories,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(ClearEntityCache<ProductCategoryResponse> command, CancellationToken cancellationToken)
        {
            _cache.Clear();

            return Task.CompletedTask;
        }

        public Task Handle(AddToCache<ProductCategoryResponse> command, CancellationToken cancellationToken)
        {
            var result = _cache.TryAdd(command.Entity.Id, command.Entity);

            if (!result)
            {
                _cache.Clear();
            }

            return Task.CompletedTask;
        }

        public Task Handle(UpdateFromCache<ProductCategoryResponse> command, CancellationToken cancellationToken)
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

        public Task Handle(RemoveFromCache<ProductCategoryResponse> command, CancellationToken cancellationToken)
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
