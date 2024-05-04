namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesTaxCategoryExistsHandler : IRequestHandler<DoesEntityExists<TaxCategoryResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesTaxCategoryExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<TaxCategoryResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.TaxCategories
                .AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id);
        }

        #endregion
    }
}
