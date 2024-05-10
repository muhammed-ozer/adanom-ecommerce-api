namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductNameExistsHandler : IRequestHandler<DoesEntityNameExists<ProductResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductNameExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityNameExists<ProductResponse> command, CancellationToken cancellationToken)
        {
            var urlSlug = command.Name.ConvertToUrlSlug();

            var query = _applicationDbContext.Products
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
