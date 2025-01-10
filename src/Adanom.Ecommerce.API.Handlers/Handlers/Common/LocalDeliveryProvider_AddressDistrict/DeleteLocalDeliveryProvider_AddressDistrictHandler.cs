using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteLocalDeliveryProvider_AddressDistrictHandler : IRequestHandler<DeleteLocalDeliveryProvider_AddressDistrict, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteLocalDeliveryProvider_AddressDistrictHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteLocalDeliveryProvider_AddressDistrict command, CancellationToken cancellationToken)
        {
            if (command.AddressDistrictId == null)
            {
                await _applicationDbContext.LocalDeliveryProvider_AddressDistrict_Mappings
                    .Where(e => e.LocalDeliveryProviderId == command.LocalDeliveryProviderId).ExecuteDeleteAsync();
            }
            else
            {
                await _applicationDbContext.LocalDeliveryProvider_AddressDistrict_Mappings
                    .Where(e => 
                        e.LocalDeliveryProviderId == command.LocalDeliveryProviderId &&
                        e.AddressDistrictId == command.AddressDistrictId) 
                    .ExecuteDeleteAsync();
            }

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = command.Identity.GetUserId(),
                    EntityType = EntityType.LOCALDELIVERYPROVIDER_ADDRESSDISTRICT,
                    TransactionType = TransactionType.DELETE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = command.Identity.GetUserId(),
                EntityType = EntityType.LOCALDELIVERYPROVIDER_ADDRESSDISTRICT,
                TransactionType = TransactionType.DELETE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful,
                $"{command.LocalDeliveryProviderId}"),
            }));

            return true;
        }

        #endregion
    }
}
