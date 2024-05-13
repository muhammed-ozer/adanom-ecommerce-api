namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_CommitTransactionBehavior : IPipelineBehavior<CreateProduct, ProductResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public CreateProduct_CommitTransactionBehavior(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<ProductResponse?> Handle(CreateProduct command, RequestHandlerDelegate<ProductResponse?> next, CancellationToken cancellationToken)
        {
            var productResponse = await next();

            var currentTransaction = _applicationDbContext.Database.CurrentTransaction;

            if (productResponse == null)
            {
                if (currentTransaction != null)
                {
                    // TODO: Log product create failed
                    await currentTransaction.RollbackAsync(cancellationToken);
                }

                return null;
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
                Log.Warning($"Product_Create_Database_Transaction_Failed: {exception.Message}");

                if (currentTransaction != null)
                {
                    await currentTransaction.RollbackAsync(cancellationToken);
                }

                productResponse = null;
            }

            return productResponse;
        }

        #endregion
    }
}
