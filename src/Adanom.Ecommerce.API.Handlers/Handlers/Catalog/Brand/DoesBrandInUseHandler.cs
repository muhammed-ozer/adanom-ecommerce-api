namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesBrandInUseHandler : IRequestHandler<DoesEntityInUse<BrandResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesBrandInUseHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityInUse<BrandResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null && e.BrandId == command.Id)
                .AnyAsync();
        }

        #endregion
    }
}
