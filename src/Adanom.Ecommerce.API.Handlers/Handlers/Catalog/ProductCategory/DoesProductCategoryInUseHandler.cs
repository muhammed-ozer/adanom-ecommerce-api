namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductCategoryInUseHandler : IRequestHandler<DoesEntityInUse<ProductCategoryResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductCategoryInUseHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityInUse<ProductCategoryResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ProductCategories
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .Where(e => 
                    e.Children.Where(e => e.DeletedAtUtc == null).Any() || 
                    e.Product_ProductCategory_Mappings.Any())
                .AnyAsync();
        }

        #endregion
    }
}
