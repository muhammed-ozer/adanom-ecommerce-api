namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesBrandExistsHandler : IRequestHandler<DoesEntityExists<BrandResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesBrandExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<BrandResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Brands
                .AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id);
        }

        #endregion
    }
}
