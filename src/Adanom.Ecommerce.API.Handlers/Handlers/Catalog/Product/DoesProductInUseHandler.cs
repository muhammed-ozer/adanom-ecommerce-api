namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductInUseHandler : IRequestHandler<DoesEntityInUse<ProductResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProductInUseHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityInUse<ProductResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.OrderItems
                .Where(e => e.ProductId == command.Id)
                .AnyAsync();
        }

        #endregion
    }
}
