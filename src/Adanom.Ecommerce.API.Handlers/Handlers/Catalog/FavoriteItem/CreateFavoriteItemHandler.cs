using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateFavoriteItemHandler : IRequestHandler<CreateFavoriteItem, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateFavoriteItemHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateFavoriteItem command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var favoriteItem = _mapper.Map<CreateFavoriteItem, FavoriteItem>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(favoriteItem);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
