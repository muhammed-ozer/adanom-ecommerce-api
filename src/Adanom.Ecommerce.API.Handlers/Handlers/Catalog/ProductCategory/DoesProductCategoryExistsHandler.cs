namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductCategoryExistsHandler : IRequestHandler<DoesEntityExists<ProductCategoryResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductCategoryExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ProductCategoryResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ProductCategories
                .AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id);
        }

        #endregion
    }
}
