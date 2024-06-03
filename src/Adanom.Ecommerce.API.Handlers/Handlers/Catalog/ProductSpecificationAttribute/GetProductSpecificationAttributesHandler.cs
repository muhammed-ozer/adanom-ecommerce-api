using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductSpecificationAttributesHandler : 
        IRequestHandler<GetProductSpecificationAttributes, PaginatedData<ProductSpecificationAttributeResponse>>,
        INotificationHandler<ClearEntityCache<ProductSpecificationAttributeResponse>>,
        INotificationHandler<AddToCache<ProductSpecificationAttributeResponse>>,
        INotificationHandler<UpdateFromCache<ProductSpecificationAttributeResponse>>,
        INotificationHandler<RemoveFromCache<ProductSpecificationAttributeResponse>>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        private readonly static ConcurrentDictionary<long, ProductSpecificationAttributeResponse> _cache = new();

        #endregion

        #region Ctor

        public GetProductSpecificationAttributesHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<ProductSpecificationAttributeResponse>> Handle(GetProductSpecificationAttributes command, CancellationToken cancellationToken)
        {
            var productSpecificationAttributesCountOnDb = await _applicationDbContext.ProductSpecificationAttributes
                .Where(e => e.DeletedAtUtc == null)
                .CountAsync();

            if (!_cache.Values.Any() || productSpecificationAttributesCountOnDb != _cache.Count)
            {
                _cache.Clear();

                var productSpecificationAttributesOnDb = await _applicationDbContext.ProductSpecificationAttributes
                   .AsNoTracking()
                   .Where(e => e.DeletedAtUtc == null)
                   .ToListAsync();

                var productSpecificationAttributeResponses = _mapper
                    .Map<IEnumerable<ProductSpecificationAttributeResponse>>(productSpecificationAttributesOnDb);

                foreach (var item in productSpecificationAttributeResponses)
                {
                    _cache.TryAdd(item.Id, item);
                }
            }

            var productSpecificationAttributes = _cache.Values.AsEnumerable();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (command.Filter.ProductSpecificationAttributeGroupId is not null)
                {
                    productSpecificationAttributes = productSpecificationAttributes
                        .Where(e => e.ProductSpecificationAttributeGroupId == command.Filter.ProductSpecificationAttributeGroupId);
                }

                if (!string.IsNullOrEmpty(command.Filter.Query))
                {
                    productSpecificationAttributes = productSpecificationAttributes
                        .Where(e => e.Name.Contains(command.Filter.Query, StringComparison.InvariantCultureIgnoreCase));
                }

                #endregion

                #region Apply ordering

                productSpecificationAttributes = command.Filter.OrderBy switch
                {
                    GetProductSpecificationAttributesOrderByEnum.DISPLAY_ORDER_DESC =>
                        productSpecificationAttributes.OrderByDescending(e => e.DisplayOrder),
                    GetProductSpecificationAttributesOrderByEnum.NAME_ASC =>
                        productSpecificationAttributes.OrderBy(e => e.Name),
                    GetProductSpecificationAttributesOrderByEnum.NAME_DESC =>
                        productSpecificationAttributes.OrderByDescending(e => e.Name),
                    _ =>
                        productSpecificationAttributes.OrderBy(e => e.DisplayOrder)
                };

                #endregion
            }
            else
            {
                productSpecificationAttributes = productSpecificationAttributes.OrderBy(e => e.DisplayOrder);
            }

            var totalCount = productSpecificationAttributes.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                productSpecificationAttributes = productSpecificationAttributes
                .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                .Take(command.Pagination.PageSize);
            }

            #endregion

            return new PaginatedData<ProductSpecificationAttributeResponse>(
                productSpecificationAttributes,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion

        #region INotificationHandler Members

        public Task Handle(ClearEntityCache<ProductSpecificationAttributeResponse> command, CancellationToken cancellationToken)
        {
            _cache.Clear();

            return Task.CompletedTask;
        }

        public Task Handle(AddToCache<ProductSpecificationAttributeResponse> command, CancellationToken cancellationToken)
        {
            var result = _cache.TryAdd(command.Entity.Id, command.Entity);

            if (!result)
            {
                _cache.Clear();
            }

            return Task.CompletedTask;
        }

        public Task Handle(UpdateFromCache<ProductSpecificationAttributeResponse> command, CancellationToken cancellationToken)
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

        public Task Handle(RemoveFromCache<ProductSpecificationAttributeResponse> command, CancellationToken cancellationToken)
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
