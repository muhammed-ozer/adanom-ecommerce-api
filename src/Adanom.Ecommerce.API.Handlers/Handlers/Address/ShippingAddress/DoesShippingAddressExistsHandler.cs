namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesShippingAddressExistsHandler : IRequestHandler<DoesUserEntityExists<ShippingAddressResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesShippingAddressExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesUserEntityExists<ShippingAddressResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ShippingAddresses
                .AnyAsync(e => e.DeletedAtUtc == null && 
                               e.UserId == command.UserId && 
                               e.Id == command.Id);
        }

        #endregion
    }
}
