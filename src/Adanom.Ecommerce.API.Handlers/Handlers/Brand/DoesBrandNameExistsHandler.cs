namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesBrandNameExistsHandler : IRequestHandler<DoesEntityNameExists<BrandResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesBrandNameExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityNameExists<BrandResponse> command, CancellationToken cancellationToken)
        {
            var urlSlug = command.Name.ConvertToUrlSlug();

            var query = _applicationDbContext.Brands
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null &&
                            e.UrlSlug == urlSlug);

            if (command.ExcludedEntityId != null)
            {
                query = query.Where(e => e.Id != command.ExcludedEntityId);
            }

            return await query.AnyAsync();
        }

        #endregion
    }
}
