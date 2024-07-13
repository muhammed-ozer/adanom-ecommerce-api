namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesBillingAddressExistsHandler : IRequestHandler<DoesUserEntityExists<BillingAddressResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesBillingAddressExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesUserEntityExists<BillingAddressResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.BillingAddresses
                .AnyAsync(e => e.DeletedAtUtc == null && 
                               e.UserId == command.UserId && 
                               e.Id == command.Id);
        }

        #endregion
    }
}
