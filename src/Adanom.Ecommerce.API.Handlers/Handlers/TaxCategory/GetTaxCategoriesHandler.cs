using System.Collections.Concurrent;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetTaxCategoriesHandler : IRequestHandler<GetTaxCategories, PaginatedData<TaxCategoryResponse>>,
        INotificationHandler<ClearEntityCache<TaxCategoryResponse>>,
        INotificationHandler<AddToCache<TaxCategoryResponse>>,
        INotificationHandler<UpdateFromCache<TaxCategoryResponse>>,
        INotificationHandler<RemoveFromCache<TaxCategoryResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, TaxCategoryResponse> _cache = new();

        #endregion

        #region Ctor

        public GetTaxCategoriesHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<TaxCategoryResponse>> Handle(GetTaxCategories command, CancellationToken cancellationToken)
        {
            var taxCategoriesCountOnDb = await _applicationDbContext.TaxCategories
                .Where(e => e.DeletedAtUtc == null)
                .CountAsync();

            if (!_cache.Values.Any() || taxCategoriesCountOnDb != _cache.Count)
            {
                _cache.Clear();

                var taxCategoriesOnDb = await _applicationDbContext.TaxCategories
                   .AsNoTracking()
                   .Where(e => e.DeletedAtUtc == null)
                   .ToListAsync();

                var brandResponses = _mapper
                    .Map<IEnumerable<TaxCategoryResponse>>(taxCategoriesOnDb);

                foreach (var item in brandResponses)
                {
                    _cache.TryAdd(item.Id, item);
                }
            }

            var taxCategories = _cache.Values.AsEnumerable();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (!string.IsNullOrEmpty(command.Filter.Query))
                {
                    taxCategories = taxCategories
                        .Where(e => 
                            e.Name.Contains(command.Filter.Query, StringComparison.InvariantCultureIgnoreCase) ||
                            e.GroupName.Contains(command.Filter.Query, StringComparison.InvariantCultureIgnoreCase));
                }

                #endregion

                #region Apply ordering

                taxCategories = command.Filter.OrderBy switch
                {
                    GetTaxCategoriesOrderByEnum.RATE_DESC =>
                        taxCategories.OrderByDescending(e => e.Rate),
                    _ =>
                        taxCategories.OrderBy(e => e.DisplayOrder)
                };

                #endregion
            }

            var totalCount = taxCategories.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                taxCategories = taxCategories
                .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                .Take(command.Pagination.PageSize);
            }

            #endregion

            return new PaginatedData<TaxCategoryResponse>(
                taxCategories,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(ClearEntityCache<TaxCategoryResponse> command, CancellationToken cancellationToken)
        {
            _cache.Clear();

            return Task.CompletedTask;
        }

        public Task Handle(AddToCache<TaxCategoryResponse> command, CancellationToken cancellationToken)
        {
            var result = _cache.TryAdd(command.Entity.Id, command.Entity);

            if (!result)
            {
                _cache.Clear();
            }

            return Task.CompletedTask;
        }

        public Task Handle(UpdateFromCache<TaxCategoryResponse> command, CancellationToken cancellationToken)
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

        public Task Handle(RemoveFromCache<TaxCategoryResponse> command, CancellationToken cancellationToken)
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
