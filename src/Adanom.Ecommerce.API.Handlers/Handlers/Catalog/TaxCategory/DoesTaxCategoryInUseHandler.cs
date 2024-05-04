namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesTaxCategoryInUseHandler : IRequestHandler<DoesEntityInUse<TaxCategoryResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesTaxCategoryInUseHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityInUse<TaxCategoryResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ProductPrices
                .Where(e => e.DeletedAtUtc == null && e.TaxCategoryId == command.Id)
                .AnyAsync();
        }

        #endregion
    }
}
