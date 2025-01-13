using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateShippingAddressHandler : IRequestHandler<CreateShippingAddress, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateShippingAddressHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateShippingAddress command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (!command.IsDefault)
            {
                var hasUserAnyOtherShippingAddress = await applicationDbContext.ShippingAddresses
                    .AsNoTracking()
                    .AnyAsync(e => e.DeletedAtUtc == null && e.UserId == userId);

                if (!hasUserAnyOtherShippingAddress)
                {
                    command.IsDefault = true;
                }
            }
            else
            {
                var currentDefaultShippingAddress = await applicationDbContext.ShippingAddresses
                    .Where(e => e.DeletedAtUtc == null &&
                                e.UserId == userId &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (currentDefaultShippingAddress != null)
                {
                    currentDefaultShippingAddress.IsDefault = false;
                }
            }

            var shippingAddress = _mapper.Map<CreateShippingAddress, ShippingAddress>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await applicationDbContext.AddAsync(shippingAddress);

            try
            {
                await applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new CustomerTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.SHIPPINGADDRESS,
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
