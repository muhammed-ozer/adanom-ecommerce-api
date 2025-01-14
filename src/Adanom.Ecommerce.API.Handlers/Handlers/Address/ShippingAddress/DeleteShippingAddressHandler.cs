using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteShippingAddressHandler : IRequestHandler<DeleteShippingAddress, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteShippingAddressHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteShippingAddress command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var shippingAddress = await applicationDbContext.ShippingAddresses
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id &&
                            e.UserId == userId)
                .SingleAsync();

            shippingAddress.DeletedAtUtc = DateTime.UtcNow;

            if (shippingAddress.IsDefault)
            {
                var randomShippingAddress = await applicationDbContext.ShippingAddresses
                    .Where(e => e.DeletedAtUtc == null &&
                                e.UserId == userId &&
                                e.Id != command.Id)
                    .FirstOrDefaultAsync();

                if (randomShippingAddress != null)
                {
                    randomShippingAddress.IsDefault = true;
                }
            }

            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
