using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteFavoriteItemHandler : IRequestHandler<DeleteFavoriteItem, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteFavoriteItemHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteFavoriteItem command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var favoriteItem = await applicationDbContext.FavoriteItems
                .Where(e => e.UserId == userId &&
                            e.Id == command.Id)
                .SingleAsync();

            applicationDbContext.Remove(favoriteItem);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
