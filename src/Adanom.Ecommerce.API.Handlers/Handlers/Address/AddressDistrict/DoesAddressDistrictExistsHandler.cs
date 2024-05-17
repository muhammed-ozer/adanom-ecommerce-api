namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesAddressDistrictExistsHandler : IRequestHandler<DoesEntityExists<AddressDistrictResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesAddressDistrictExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<AddressDistrictResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.AddressDistricts.AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
