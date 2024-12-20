namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductsHandler : IRequestHandler<GetProducts, ProductCatalogResponse>
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

        public async Task<ProductCatalogResponse> Handle(GetProducts command, CancellationToken cancellationToken)
        {
            var productsQuery = _applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null)
                .AsNoTracking();

            var productCatalogResponse = new ProductCatalogResponse();

            if (command.Filter != null)
            {
                #region Apply filtering

                if (command.Filter.ProductCategoryUrlSlugs != null && command.Filter.ProductCategoryUrlSlugs.Count > 0)
                {
                    var childProductCategoriesIds = await GetChildProductCategoriesIdsAsync(command.Filter.ProductCategoryUrlSlugs);

                    productsQuery = _applicationDbContext.Product_ProductCategory_Mappings
                                                .Where(e => childProductCategoriesIds.Contains(e.ProductCategoryId))
                                                .Select(e => e.Product)
                                                .Distinct();
                }

                if (command.Filter.ProductSpecificationAttributeIds != null && command.Filter.ProductSpecificationAttributeIds.Count > 0)
                {
                    productsQuery = _applicationDbContext.Product_ProductSpecificationAttribute_Mappings
                                                .Where(e => command.Filter.ProductSpecificationAttributeIds.Contains(e.ProductSpecificationAttributeId))
                                                .Select(e => e.Product)
                                                .Distinct();
                }

                if (command.Filter.BrandUrlSlugs != null && command.Filter.BrandUrlSlugs.Count > 0)
                {
                    productsQuery = _applicationDbContext.Products
                      .Where(p => p.BrandId.HasValue && _applicationDbContext.Brands
                          .Where(b => command.Filter.BrandUrlSlugs.Contains(b.UrlSlug))
                          .Select(b => b.Id)
                          .ToList()
                          .Contains(p.BrandId.Value));
                }

                if (!string.IsNullOrEmpty(command.Filter.Query))
                {
                    if (command.Filter.IsRequestFromStoreClient != null && command.Filter.IsRequestFromStoreClient.Value)
                    {
                        productsQuery = productsQuery.Where(e =>
                           e.Name.Contains(command.Filter.Query) ||
                           e.ProductSKU.Code.Contains(command.Filter.Query) ||
                           e.Product_ProductTag_Mappings.Any(e => e.ProductTag.Value.Contains(command.Filter.Query)));
                    }
                    else
                    {
                        productsQuery = productsQuery.Where(e =>
                           e.Name.Contains(command.Filter.Query) ||
                           e.ProductSKU.Code.Contains(command.Filter.Query));
                    }
                }

                if (command.Filter.OutOfStock != null)
                {
                    if (command.Filter.OutOfStock.Value)
                    {
                        productsQuery = productsQuery.Where(e => e.ProductSKU.StockQuantity == 0);
                    }
                    else
                    {
                        productsQuery = productsQuery.Where(e => e.ProductSKU.StockQuantity > 0);
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

                if (command.Filter.IsInHighlights != null)
                {
                    productsQuery = productsQuery.Where(e => e.IsInHighlights);
                }

                if (command.Filter.IsInProductsOfTheWeek != null)
                {
                    if (command.Filter.IsInProductsOfTheWeek.Value)
                    {
                        productsQuery = productsQuery.Where(e => e.IsInProductsOfTheWeek);
                    }
                    else
                    {
                        productsQuery = productsQuery.Where(e => !e.IsInProductsOfTheWeek);
                    }
                }

                if (command.Filter.MinimumPrice != null)
                {
                    productsQuery = productsQuery.Where(e => 
                            e.ProductSKU.ProductPrice.OriginalPrice > command.Filter.MinimumPrice ||
                            e.ProductSKU.ProductPrice.DiscountedPrice > command.Filter.MinimumPrice);
                }

                if (command.Filter.MaximumPrice != null)
                {
                    productsQuery = productsQuery.Where(e =>
                            e.ProductSKU.ProductPrice.OriginalPrice < command.Filter.MaximumPrice ||
                            e.ProductSKU.ProductPrice.DiscountedPrice < command.Filter.MaximumPrice);
                }

                if (command.Filter.ReviewPoint != null)
                {
                    productsQuery.Where(e => e.ProductReviews.Any(e => e.Points == command.Filter.ReviewPoint.Value));
                }

                if (command.Filter.IncludeFilterResponse != null && command.Filter.IncludeFilterResponse.Value)
                {
                    productCatalogResponse.ProductFilterResponse = await GetProductFilterResponseAsync(productsQuery);
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
                        productsQuery.OrderBy(e => e.ProductSKU.StockQuantity),
                    GetProductsOrderByEnum.STOCK_QUANTITY_DESC =>
                        productsQuery.OrderByDescending(e => e.ProductSKU.StockQuantity),
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

            productCatalogResponse.PaginatedDataOfProducts = 
                new PaginatedData<ProductResponse>(
                    productResponses,
                    totalCount,
                    command.Pagination?.Page ?? 1,
                    command.Pagination?.PageSize ?? totalCount);

            return productCatalogResponse;
        }

        #endregion

        #region Private Methods

        #region GetChildProductCategoriesIdsAsync

        private async Task<List<long>> GetChildProductCategoriesIdsAsync(ICollection<string> urlSlugs)
        {
            // Get the parent categories based on the provided URL slugs
            var parentCategories = await _applicationDbContext.ProductCategories
                .Where(e => e.DeletedAtUtc == null && urlSlugs.Contains(e.UrlSlug))
                .ToListAsync();

            if (!parentCategories.Any())
            {
                return [];
            }

            var allProductCategories = await _applicationDbContext.ProductCategories
                .Where(e => e.DeletedAtUtc == null)
                .ToListAsync();

            List<long> childProductCategoriesIds = new List<long>();

            // Helper method that finds children recursively
            void FindChildren(long parentId)
            {
                var children = allProductCategories.Where(c => c.DeletedAtUtc == null && c.ParentId == parentId).ToList();

                foreach (var child in children)
                {
                    childProductCategoriesIds.Add(child.Id);
                    FindChildren(child.Id);
                }
            }

            // Find all categories and their children for each parent category
            foreach (var parentCategory in parentCategories)
            {
                childProductCategoriesIds.Add(parentCategory.Id); // Add the parent category itself
                FindChildren(parentCategory.Id);
            }

            return childProductCategoriesIds;
        }  

        #endregion

        private async Task<ProductFilterResponse> GetProductFilterResponseAsync(IQueryable<Product> productsQuery)
        {
            var productFilterResponse = new ProductFilterResponse();

            if (!await productsQuery.AnyAsync())
            {
                return productFilterResponse;
            }

            productFilterResponse.MinimumPrice = await productsQuery.MinAsync(e => 
                e.ProductSKU.ProductPrice.DiscountedPrice.HasValue ? 
                e.ProductSKU.ProductPrice.DiscountedPrice.Value : 
                e.ProductSKU.ProductPrice.OriginalPrice);

            productFilterResponse.MaximumPrice = await productsQuery.MaxAsync(e =>
                e.ProductSKU.ProductPrice.DiscountedPrice.HasValue ?
                e.ProductSKU.ProductPrice.DiscountedPrice.Value :
                e.ProductSKU.ProductPrice.OriginalPrice);

            var productCategoriesQuery = productsQuery
                .SelectMany(e => e.Product_ProductCategory_Mappings)
                .Select(e => e.ProductCategory)
                .Where(e => e.DeletedAtUtc == null)
                .Distinct();

            productFilterResponse.ProductCategories = _mapper
                .Map<IEnumerable<ProductCategoryResponse>>(await productCategoriesQuery.ToListAsync());

            var productSpecificationAttributesQuery = productsQuery
                .SelectMany(e => e.Product_ProductSpecificationAttribute_Mappings
                .Select(e => e.ProductSpecificationAttribute))
                .Where(e => e.DeletedAtUtc == null)
                .Distinct();

            productFilterResponse.ProductSpecificationAttributes = _mapper
                .Map<IEnumerable<ProductSpecificationAttributeResponse>>(await productSpecificationAttributesQuery.ToListAsync());

            var brandsQuery = productsQuery
                .Where(e => e.BrandId != null)
                .Select(e => e.Brand)
                .Where(e => e.DeletedAtUtc == null)
                .Distinct();

            productFilterResponse.Brands = _mapper
                .Map<IEnumerable<BrandResponse>>(await brandsQuery.ToListAsync());

            return productFilterResponse;
        }

        #endregion
    }
}
