namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesAddressDistrictExistsHandler : IRequestHandler<DoesEntityExists<AddressDistrictResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesAddressDistrictExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<AddressDistrictResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            return await applicationDbContext.AddressDistricts.AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
