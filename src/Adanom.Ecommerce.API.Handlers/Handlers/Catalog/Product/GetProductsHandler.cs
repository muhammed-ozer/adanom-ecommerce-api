namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductsHandler : IRequestHandler<GetProducts, PaginatedData<ProductResponse>>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<ProductResponse>> Handle(GetProducts command, CancellationToken cancellationToken)
        {
            var productsQuery = _applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null)
                .AsNoTracking();

            if (command.Filter != null)
            {
                #region Apply filtering

                if (command.Filter.ProductCategoryId != null)
                {
                    var childProductCategoriesIds = await GetChildProductCategoriesIdsAsync(command.Filter.ProductCategoryId.Value);

                    productsQuery = _applicationDbContext.Product_ProductCategory_Mappings
                                                .Where(e => childProductCategoriesIds.Contains(e.ProductCategoryId))
                                                .Select(e => e.Product)
                                                .Distinct();
                }

                if (!string.IsNullOrEmpty(command.Filter.Query))
                {
                    productsQuery = productsQuery.Where(e => e.Name.Contains(command.Filter.Query));
                }

                if (command.Filter.OutOfStock != null)
                {
                    if (command.Filter.OutOfStock.Value)
                    {
                        productsQuery = productsQuery.Where(e => e.ProductSKUs.Any(e => e.StockQuantity == 0));
                    }
                    else
                    {
                        productsQuery = productsQuery.Where(e => e.ProductSKUs.Any(e => e.StockQuantity > 0));
                    }
                }

                if (command.Filter.IsActive != null)
                {
                    if (command.Filter.IsActive.Value)
                    {
                        productsQuery = productsQuery.Where(e => e.IsActive);
                    }
                    else
                    {
                        productsQuery = productsQuery.Where(e => !e.IsActive);
                    }
                }

                if (command.Filter.IsNew != null)
                {
                    if (command.Filter.IsNew.Value)
                    {
                        productsQuery = productsQuery.Where(e => e.IsNew);
                    }
                    else
                    {
                        productsQuery = productsQuery.Where(e => !e.IsNew);
                    }
                }

                #endregion

                #region Apply ordering

                productsQuery = command.Filter.OrderBy switch
                {
                    GetProductsOrderByEnum.DISPLAY_ORDER_DESC =>
                        productsQuery.OrderByDescending(e => e.DisplayOrder),
                    GetProductsOrderByEnum.NAME_ASC =>
                        productsQuery.OrderBy(e => e.Name),
                    GetProductsOrderByEnum.NAME_DESC =>
                        productsQuery.OrderByDescending(e => e.Name),
                    GetProductsOrderByEnum.STOCK_QUANTITY_ASC =>
                        productsQuery.OrderBy(e => e.ProductSKUs
                            .Where(e => e.DeletedAtUtc == null)
                            .Sum(e => e.StockQuantity)),
                    GetProductsOrderByEnum.STOCK_QUANTITY_DESC =>
                        productsQuery.OrderByDescending(e => e.ProductSKUs
                            .Where(e => e.DeletedAtUtc == null)
                            .Sum(e => e.StockQuantity)),
                    _ =>
                        productsQuery.OrderBy(e => e.DisplayOrder)
                };

                #endregion
            }
            else
            {
                productsQuery = productsQuery.OrderBy(e => e.DisplayOrder);
            }

            var totalCount = productsQuery.Count();

            #region Apply pagination

            productsQuery = productsQuery
                .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                .Take(command.Pagination.PageSize);

            #endregion

            var productResponses = _mapper.Map<IEnumerable<ProductResponse>>(await productsQuery.ToListAsync());

            return new PaginatedData<ProductResponse>(
                productResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion

        #region Private Methods

        private async Task<List<long>> GetChildProductCategoriesIdsAsync(long categoryId)
        {
            var allProductCategories = await _applicationDbContext.ProductCategories.ToListAsync();

            List<long> childProductCategoriesIds = new List<long>();

            // Helper method that finds children recursively
            void FindChildren(long parentId)
            {
                var children = allProductCategories.Where(c => c.ParentId == parentId).ToList();

                foreach (var child in children)
                {
                    childProductCategoriesIds.Add(child.Id);
                    FindChildren(child.Id);
                }
            }

            // Find the first category and all its children
            childProductCategoriesIds.Add(categoryId); // Add yourself

            FindChildren(categoryId);

            return childProductCategoriesIds;
        } 

        #endregion
    }
}
