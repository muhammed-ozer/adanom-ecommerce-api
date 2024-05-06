namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductSpecificationAttributeExistsHandler : IRequestHandler<DoesEntityExists<ProductSpecificationAttributeResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductSpecificationAttributeExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ProductSpecificationAttributeResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ProductSpecificationAttributes
                .AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id);
        }

        #endregion
    }
}
