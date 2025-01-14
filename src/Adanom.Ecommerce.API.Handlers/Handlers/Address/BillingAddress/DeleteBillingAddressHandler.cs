using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteBillingAddressHandler : IRequestHandler<DeleteBillingAddress, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteBillingAddressHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteBillingAddress command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var billingAddress = await applicationDbContext.BillingAddresses
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id &&
                            e.UserId == userId)
                .SingleAsync();

            billingAddress.DeletedAtUtc = DateTime.UtcNow;

            if (billingAddress.IsDefault)
            {
                var randomBillingAddress = await applicationDbContext.BillingAddresses
                    .Where(e => e.DeletedAtUtc == null &&
                                e.UserId == userId &&
                                e.Id != command.Id)
                    .FirstOrDefaultAsync();

                if (randomBillingAddress != null)
                {
                    randomBillingAddress.IsDefault = true;
                }
            }

            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
