namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductSKUExistsHandler : IRequestHandler<DoesEntityExists<ProductSKUResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductSKUExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ProductSKUResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ProductSKUs
                .AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id);
        }

        #endregion
    }
}
