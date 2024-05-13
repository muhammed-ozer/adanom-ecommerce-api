namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProduct_ProductCategoryExistsHandler : IRequestHandler<DoesProduct_ProductCategoryExists, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProduct_ProductCategoryExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProduct_ProductCategoryExists command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Product_ProductCategory_Mappings
                .AnyAsync(e => 
                    e.ProductId == command.ProductId && 
                    e.ProductCategoryId == command.ProductCategoryId);
        }

        #endregion
    }
}
