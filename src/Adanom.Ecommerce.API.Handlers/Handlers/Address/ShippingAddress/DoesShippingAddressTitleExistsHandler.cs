namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesShippingAddressTitleExistsHandler : IRequestHandler<DoesUserEntityNameExists<ShippingAddressResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesShippingAddressTitleExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesUserEntityNameExists<ShippingAddressResponse> command, CancellationToken cancellationToken)
        {
            var title = command.Name.ToLower();

            var query = _applicationDbContext.ShippingAddresses
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null &&
                            e.UserId == command.UserId &&
                            e.Title == title);

            if (command.ExcludedEntityId != null)
            {
                query = query.Where(e => e.Id != command.ExcludedEntityId);
            }

            return await query.AnyAsync();
        }

        #endregion
    }
}
