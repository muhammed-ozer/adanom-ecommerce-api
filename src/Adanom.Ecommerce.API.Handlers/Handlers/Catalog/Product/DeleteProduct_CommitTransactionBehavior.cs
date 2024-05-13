namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProduct_CommitTransactionBehavior : IPipelineBehavior<DeleteProduct, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DeleteProduct_CommitTransactionBehavior(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteProduct command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteProductResponse = await next();

            var currentTransaction = _applicationDbContext.Database.CurrentTransaction;

            if (!deleteProductResponse)
            {
                if (currentTransaction != null)
                {
                    // TODO: Log product delete failed
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
                Log.Warning($"Product_Delete_Database_Transaction_Failed: {exception.Message}");

                if (currentTransaction != null)
                {
                    await currentTransaction.RollbackAsync(cancellationToken);
                }

                deleteProductResponse = false;
            }

            return deleteProductResponse;
        }

        #endregion
    }
}
