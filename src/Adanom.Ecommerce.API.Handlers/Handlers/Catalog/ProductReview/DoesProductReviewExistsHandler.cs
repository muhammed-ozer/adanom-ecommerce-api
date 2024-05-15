namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductReviewExistsHandler : IRequestHandler<DoesEntityExists<ProductReviewResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductReviewExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ProductReviewResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ProductReviews.AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
