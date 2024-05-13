namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductSKUCodeExistsHandler : IRequestHandler<DoesProductSKUCodeExists, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductSKUCodeExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProductSKUCodeExists command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ProductSKUs
                .AnyAsync(e => e.DeletedAtUtc == null && e.Code.ToUpper() == command.Code.ToUpper());
        }

        #endregion
    }
}
