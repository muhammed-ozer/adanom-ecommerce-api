using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteBillingAddressHandler : IRequestHandler<DeleteBillingAddress, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteBillingAddressHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteBillingAddress command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var billingAddress = await _applicationDbContext.BillingAddresses
                .Where(e => e.DeletedAtUtc == null && 
                            e.Id == command.Id && 
                            e.UserId == userId)
                .SingleAsync();

            billingAddress.DeletedAtUtc = DateTime.UtcNow;

            if (billingAddress.IsDefault)
            {
                var randomBillingAddress = await _applicationDbContext.BillingAddresses
                    .Where(e => e.DeletedAtUtc == null && 
                                e.UserId == userId && 
                                e.Id != command.Id)
                    .FirstOrDefaultAsync();

                if (randomBillingAddress != null)
                {
                    randomBillingAddress.IsDefault = true;
                }
            }

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.BILLINGADDRESS,
                    TransactionType = TransactionType.DELETE,
                    Description = LogMessages.CustomerTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            return true;
        }

        #endregion
    }
}
