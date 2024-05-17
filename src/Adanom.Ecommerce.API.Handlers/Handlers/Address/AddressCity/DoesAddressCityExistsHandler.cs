namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesAddressCityExistsHandler : IRequestHandler<DoesEntityExists<AddressCityResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesAddressCityExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<AddressCityResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.AddressCities.AnyAsync(e => e.Id == command.Id);
        }

        #endregion
    }
}
