using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UserCanCreateProductReviewHandler : IRequestHandler<UserCanCreateProductReview, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public UserCanCreateProductReviewHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UserCanCreateProductReview command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.Orders
                .Where(e => e.UserId == userId && e.Items.Any(e => e.ProductId == command.ProductId))
                .AnyAsync(cancellationToken);
        }

        #endregion
    }
}
