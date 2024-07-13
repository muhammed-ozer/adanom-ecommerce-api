using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteShippingAddressHandler : IRequestHandler<DeleteShippingAddress, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteShippingAddressHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteShippingAddress command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var shippingAddress = await _applicationDbContext.ShippingAddresses
                .Where(e => e.DeletedAtUtc == null && 
                            e.Id == command.Id && 
                            e.UserId == userId)
                .SingleAsync();

            shippingAddress.DeletedAtUtc = DateTime.UtcNow;

            if (shippingAddress.IsDefault)
            {
                var randomShippingAddress = await _applicationDbContext.ShippingAddresses
                    .Where(e => e.DeletedAtUtc == null && 
                                e.UserId == userId && 
                                e.Id != command.Id)
                    .FirstOrDefaultAsync();

                if (randomShippingAddress != null)
                {
                    randomShippingAddress.IsDefault = true;
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
                    EntityType = EntityType.SHIPPINGADDRESS,
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
