namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesLocalDeliveryProviderExistsHandler : IRequestHandler<DoesEntityExists<LocalDeliveryProviderResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesLocalDeliveryProviderExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<LocalDeliveryProviderResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.LocalDeliveryProviders
                .AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id);
        }

        #endregion
    }
}
