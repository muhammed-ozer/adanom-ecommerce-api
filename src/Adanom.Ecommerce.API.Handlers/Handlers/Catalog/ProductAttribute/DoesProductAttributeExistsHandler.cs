namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductAttributeExistsHandler : IRequestHandler<DoesEntityExists<ProductAttributeResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductAttributeExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ProductAttributeResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ProductAttributes
                .AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id);
        }

        #endregion
    }
}
