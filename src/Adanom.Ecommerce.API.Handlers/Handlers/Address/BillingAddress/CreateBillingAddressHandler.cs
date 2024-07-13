using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateBillingAddressHandler : IRequestHandler<CreateBillingAddress, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateBillingAddressHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateBillingAddress command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            if (!command.IsDefault)
            {
                var hasUserAnyOtherBillingAddress = await _applicationDbContext.BillingAddresses
                    .AsNoTracking()
                    .AnyAsync(e => e.DeletedAtUtc == null && e.UserId == userId);

                if (!hasUserAnyOtherBillingAddress)
                {
                    command.IsDefault = true;
                }
            }
            else
            {
                var currentDefaultBillingAddress = await _applicationDbContext.BillingAddresses
                    .Where(e => e.DeletedAtUtc == null &&
                                e.UserId == userId &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (currentDefaultBillingAddress != null)
                {
                    currentDefaultBillingAddress.IsDefault = false;
                }
            }

            var billingAddress = _mapper.Map<CreateBillingAddress, BillingAddress>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await _applicationDbContext.AddAsync(billingAddress);

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
                    TransactionType = TransactionType.CREATE,
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
