namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProduct_ProductCategoryHandler : IRequestHandler<DeleteProduct_ProductCategory, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DeleteProduct_ProductCategoryHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProduct_ProductCategory command, CancellationToken cancellationToken)
        {
            var product_ProductCategory = await _applicationDbContext.Product_ProductCategory_Mappings
                .Where(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductCategoryId == command.ProductCategoryId)
                .SingleAsync();

            _applicationDbContext.Remove(product_ProductCategory);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"Product_ProductCategory_Delete_Failed: {exception.Message}");

                return false;
            }

            return true;
        }

        #endregion
    }
}
