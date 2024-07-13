using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateShippingAddressHandler : IRequestHandler<CreateShippingAddress, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateShippingAddressHandler(
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

        public async Task<bool> Handle(CreateShippingAddress command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            if (!command.IsDefault)
            {
                var hasUserAnyOtherShippingAddress = await _applicationDbContext.ShippingAddresses
                    .AsNoTracking()
                    .AnyAsync(e => e.DeletedAtUtc == null && e.UserId == userId);

                if (!hasUserAnyOtherShippingAddress)
                {
                    command.IsDefault = true;
                }
            }
            else
            {
                var currentDefaultShippingAddress = await _applicationDbContext.ShippingAddresses
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

            await _applicationDbContext.AddAsync(shippingAddress);

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
