namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProduct_ProductTagHandler : IRequestHandler<DeleteProduct_ProductTag, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DeleteProduct_ProductTagHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProduct_ProductTag command, CancellationToken cancellationToken)
        {
            var product_ProductTag = await _applicationDbContext.Product_ProductTag_Mappings
                .Where(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductTagId == command.ProductTagId)
                .SingleOrDefaultAsync();

            if (product_ProductTag == null)
            {
                return true;
            }

            _applicationDbContext.Remove(product_ProductTag);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"Product_ProductTag_Delete_Failed: {exception.Message}");

                return false;
            }

            return true;
        }

        #endregion
    }
}
