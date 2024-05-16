namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesTaxAdministrationExistsHandler : IRequestHandler<DoesEntityExists<TaxAdministrationResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesTaxAdministrationExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<TaxAdministrationResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.TaxAdministrations
                .AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id);
        }

        #endregion
    }
}
