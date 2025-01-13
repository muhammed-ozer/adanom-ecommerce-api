namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductIsActiveHandler : IRequestHandler<DoesProductIsActive, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProductIsActiveHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProductIsActive command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .AnyAsync(e => e.IsActive);
        }

        #endregion
    }
}
