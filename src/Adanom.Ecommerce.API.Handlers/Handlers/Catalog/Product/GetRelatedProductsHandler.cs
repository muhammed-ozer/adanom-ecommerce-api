namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetRelatedProductsHandler : IRequestHandler<GetRelatedProducts, IEnumerable<ProductResponse>>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetRelatedProductsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ProductResponse>> Handle(GetRelatedProducts command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productWithRelations = await applicationDbContext.Products
                .AsNoTracking()
                .Include(p => p.Product_ProductCategory_Mappings)
                .Include(p => p.Product_ProductTag_Mappings)
                .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

            if (productWithRelations == null)
            {
                return [];
            }

            var categoryIds = productWithRelations.Product_ProductCategory_Mappings
                .Select(pc => pc.ProductCategoryId)
                .ToList();

            var tagIds = productWithRelations.Product_ProductTag_Mappings
                .Select(pt => pt.ProductTagId)
                .ToList();

            // Query for related products
            var relatedProductsQuery = applicationDbContext.Products
                .AsNoTracking()
                .Where(p => p.Id != command.Id && p.IsActive && !p.DeletedAtUtc.HasValue)
                .Where(p =>
                    p.Product_ProductCategory_Mappings.Any(pc => categoryIds.Contains(pc.ProductCategoryId)) ||
                    p.Product_ProductTag_Mappings.Any(pt => tagIds.Contains(pt.ProductTagId)))
                .OrderByDescending(p =>
                    p.Product_ProductCategory_Mappings.Count(pc => categoryIds.Contains(pc.ProductCategoryId)) +
                    p.Product_ProductTag_Mappings.Count(pt => tagIds.Contains(pt.ProductTagId)))
                .Take(6);

            var relatedProductResponses = _mapper.Map<IEnumerable<ProductResponse>>(await relatedProductsQuery.ToListAsync(cancellationToken));

            return relatedProductResponses;
        }

        #endregion
    }
}
