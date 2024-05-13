namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductSKU_CommitTransactionBehavior : IPipelineBehavior<DeleteProductSKU, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DeleteProductSKU_CommitTransactionBehavior(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteProductSKU command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteProductSKUResponse = await next();

            var currentTransaction = _applicationDbContext.Database.CurrentTransaction;

            if (!deleteProductSKUResponse)
            {
                if (currentTransaction != null)
                {
                    // TODO: Log product SKU delete failed
                    await currentTransaction.RollbackAsync(cancellationToken);
                }

                return false;
            }

            try
            {
                if (currentTransaction != null)
                {
                    await currentTransaction.CommitAsync();
                }
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"ProductSKU_Delete_Database_Transaction_Failed: {exception.Message}");

                if (currentTransaction != null)
                {
                    await currentTransaction.RollbackAsync(cancellationToken);
                }

                deleteProductSKUResponse = false;
            }

            return deleteProductSKUResponse;
        }

        #endregion
    }
}
