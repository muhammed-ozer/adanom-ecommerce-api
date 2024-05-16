namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesTaxAdministrationCodeExistsHandler : IRequestHandler<DoesTaxAdministrationCodeExists, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesTaxAdministrationCodeExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesTaxAdministrationCodeExists command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.TaxAdministrations
                .AnyAsync(e => e.DeletedAtUtc == null && e.Code.ToUpper() == command.Code.ToUpper());
        }

        #endregion
    }
}
