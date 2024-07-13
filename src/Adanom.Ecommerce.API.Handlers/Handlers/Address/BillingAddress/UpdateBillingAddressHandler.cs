using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateBillingAddressHandler : IRequestHandler<UpdateBillingAddress, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateBillingAddressHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateBillingAddress command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var billingAddress = await _applicationDbContext.BillingAddresses
                .Where(e => e.DeletedAtUtc == null &&
                            e.UserId == userId &&
                            e.Id == command.Id)
                .SingleAsync();

            if (command.IsDefault && !billingAddress.IsDefault)
            {
                var cuurentDefaultBillingAddress = await _applicationDbContext.BillingAddresses
                    .Where(e => e.DeletedAtUtc == null &&
                                e.UserId == userId &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (cuurentDefaultBillingAddress != null)
                {
                    cuurentDefaultBillingAddress.IsDefault = false;
                }
            }
            else
            {
                command.IsDefault = true;
            }

            billingAddress = _mapper.Map(command, billingAddress, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            _applicationDbContext.Update(billingAddress);

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
                    TransactionType = TransactionType.UPDATE,
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
