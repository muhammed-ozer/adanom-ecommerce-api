using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateShippingAddressHandler : IRequestHandler<UpdateShippingAddress, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateShippingAddressHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateShippingAddress command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var shippingAddress = await applicationDbContext.ShippingAddresses
                .Where(e => e.DeletedAtUtc == null &&
                            e.UserId == userId &&
                            e.Id == command.Id)
                .SingleAsync();

            if (command.IsDefault && !shippingAddress.IsDefault)
            {
                var curentDefaultShippingAddress = await applicationDbContext.ShippingAddresses
                    .Where(e => e.DeletedAtUtc == null &&
                                e.UserId == userId &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (curentDefaultShippingAddress != null)
                {
                    curentDefaultShippingAddress.IsDefault = false;
                }
            }
            else if (!command.IsDefault && shippingAddress.IsDefault)
            {
                shippingAddress.IsDefault = false;
            }
            else
            {
                command.IsDefault = false;
            }

            shippingAddress = _mapper.Map(command, shippingAddress, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            applicationDbContext.Update(shippingAddress);

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
