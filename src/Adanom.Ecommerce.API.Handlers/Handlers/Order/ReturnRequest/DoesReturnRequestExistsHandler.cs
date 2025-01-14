namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesReturnRequestExistsHandler : IRequestHandler<DoesEntityExists<ReturnRequestResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesReturnRequestExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ReturnRequestResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.ReturnRequests
                .AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
