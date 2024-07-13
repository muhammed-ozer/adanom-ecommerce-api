using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateShippingAddressHandler : IRequestHandler<UpdateShippingAddress, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateShippingAddressHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateShippingAddress command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var shippingAddress = await _applicationDbContext.ShippingAddresses
                .Where(e => e.DeletedAtUtc == null &&
                            e.UserId == userId &&
                            e.Id == command.Id)
                .SingleAsync();

            if (command.IsDefault && !shippingAddress.IsDefault)
            {
                var cuurentDefaultShippingAddress = await _applicationDbContext.ShippingAddresses
                    .Where(e => e.DeletedAtUtc == null &&
                                e.UserId == userId &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (cuurentDefaultShippingAddress != null)
                {
                    cuurentDefaultShippingAddress.IsDefault = false;
                }
            }
            else
            {
                command.IsDefault = true;
            }

            shippingAddress = _mapper.Map(command, shippingAddress, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            _applicationDbContext.Update(shippingAddress);

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
