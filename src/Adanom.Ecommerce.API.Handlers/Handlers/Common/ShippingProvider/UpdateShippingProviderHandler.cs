using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateShippingProviderHandler : IRequestHandler<UpdateShippingProvider, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateShippingProviderHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateShippingProvider command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var shippingProvider = await _applicationDbContext.ShippingProviders
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            if (command.IsDefault && !shippingProvider.IsDefault)
            {
                var cuurentDefaultShippingprovider = await _applicationDbContext.ShippingProviders
                    .Where(e => e.DeletedAtUtc == null &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (cuurentDefaultShippingprovider != null)
                {
                    cuurentDefaultShippingprovider.IsDefault = false;
                }
            }
            else
            {
                command.IsDefault = true;
            }

            shippingProvider = _mapper.Map(command, shippingProvider, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedByUserId = userId;
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            _applicationDbContext.Update(shippingProvider);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.SHIPPINGPROVIDER,
                    TransactionType = TransactionType.UPDATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new ClearEntityCache<ShippingProviderResponse>());

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.SHIPPINGPROVIDER,
                TransactionType = TransactionType.UPDATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, shippingProvider.Id),
            }));

            return true;
        }

        #endregion
    }
}
