using System.Collections.Concurrent;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductSpecificationAttributeGroupsHandler :
        IRequestHandler<GetProductSpecificationAttributeGroups, PaginatedData<ProductSpecificationAttributeGroupResponse>>,
        INotificationHandler<ClearEntityCache<ProductSpecificationAttributeGroupResponse>>,
        INotificationHandler<AddToCache<ProductSpecificationAttributeGroupResponse>>,
        INotificationHandler<UpdateFromCache<ProductSpecificationAttributeGroupResponse>>,
        INotificationHandler<RemoveFromCache<ProductSpecificationAttributeGroupResponse>>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, ProductSpecificationAttributeGroupResponse> _cache = new();

        #endregion

        #region Ctor

        public GetProductSpecificationAttributeGroupsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<ProductSpecificationAttributeGroupResponse>> Handle(GetProductSpecificationAttributeGroups command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productSpecificationAttributeGroupsCountOnDb = await applicationDbContext.ProductSpecificationAttributeGroups
                .Where(e => e.DeletedAtUtc == null)
                .CountAsync();

            if (!_cache.Values.Any() || productSpecificationAttributeGroupsCountOnDb != _cache.Count)
            {
                _cache.Clear();

                var productSpecificationAttributeGroupsOnDb = await applicationDbContext.ProductSpecificationAttributeGroups
                   .AsNoTracking()
                   .Where(e => e.DeletedAtUtc == null)
                   .ToListAsync();

                var productSpecificationAttributeGroupResponses = _mapper
                    .Map<IEnumerable<ProductSpecificationAttributeGroupResponse>>(productSpecificationAttributeGroupsOnDb);

                foreach (var item in productSpecificationAttributeGroupResponses)
                {
                    _cache.TryAdd(item.Id, item);
                }
            }

            var productSpecificationAttributeGroups = _cache.Values.AsEnumerable();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (!string.IsNullOrEmpty(command.Filter.Query))
                {
                    productSpecificationAttributeGroups = productSpecificationAttributeGroups
                        .Where(e => e.Name.Contains(command.Filter.Query, StringComparison.InvariantCultureIgnoreCase));
                }

                #endregion

                #region Apply ordering

                productSpecificationAttributeGroups = command.Filter.OrderBy switch
                {
                    GetProductSpecificationAttributeGroupsOrderByEnum.DISPLAY_ORDER_DESC =>
                        productSpecificationAttributeGroups.OrderByDescending(e => e.DisplayOrder),
                    GetProductSpecificationAttributeGroupsOrderByEnum.NAME_ASC =>
                        productSpecificationAttributeGroups.OrderBy(e => e.Name),
                    GetProductSpecificationAttributeGroupsOrderByEnum.NAME_DESC =>
                        productSpecificationAttributeGroups.OrderByDescending(e => e.Name),
                    _ =>
                        productSpecificationAttributeGroups.OrderBy(e => e.DisplayOrder)
                };

                #endregion
            }
            else
            {
                productSpecificationAttributeGroups = productSpecificationAttributeGroups.OrderBy(e => e.DisplayOrder);
            }

            var totalCount = productSpecificationAttributeGroups.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                productSpecificationAttributeGroups = productSpecificationAttributeGroups
                .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                .Take(command.Pagination.PageSize);
            }

            #endregion

            return new PaginatedData<ProductSpecificationAttributeGroupResponse>(
                productSpecificationAttributeGroups,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(ClearEntityCache<ProductSpecificationAttributeGroupResponse> command, CancellationToken cancellationToken)
        {
            _cache.Clear();

            return Task.CompletedTask;
        }

        public Task Handle(AddToCache<ProductSpecificationAttributeGroupResponse> command, CancellationToken cancellationToken)
        {
            var result = _cache.TryAdd(command.Entity.Id, command.Entity);

            if (!result)
            {
                _cache.Clear();
            }

            return Task.CompletedTask;
        }

        public Task Handle(UpdateFromCache<ProductSpecificationAttributeGroupResponse> command, CancellationToken cancellationToken)
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

        public Task Handle(RemoveFromCache<ProductSpecificationAttributeGroupResponse> command, CancellationToken cancellationToken)
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
