namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductReviewExistsHandler : IRequestHandler<DoesEntityExists<ProductReviewResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProductReviewExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ProductReviewResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.ProductReviews.AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
