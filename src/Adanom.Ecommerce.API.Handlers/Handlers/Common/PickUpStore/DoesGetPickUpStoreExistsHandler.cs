namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesPickUpStoreExistsHandler : IRequestHandler<DoesEntityExists<PickUpStoreResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesPickUpStoreExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<PickUpStoreResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.PickUpStores
                .AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id);
        }

        #endregion
    }
}
