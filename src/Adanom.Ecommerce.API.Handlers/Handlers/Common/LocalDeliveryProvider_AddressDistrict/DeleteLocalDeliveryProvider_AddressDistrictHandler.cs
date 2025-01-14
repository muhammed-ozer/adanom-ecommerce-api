using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteLocalDeliveryProvider_AddressDistrictHandler : IRequestHandler<DeleteLocalDeliveryProvider_AddressDistrict, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteLocalDeliveryProvider_AddressDistrictHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteLocalDeliveryProvider_AddressDistrict command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (command.AddressDistrictId == null)
            {
                await applicationDbContext.LocalDeliveryProvider_AddressDistrict_Mappings
                    .Where(e => e.LocalDeliveryProviderId == command.LocalDeliveryProviderId).ExecuteDeleteAsync();
            }
            else
            {
                await applicationDbContext.LocalDeliveryProvider_AddressDistrict_Mappings
                    .Where(e =>
                        e.LocalDeliveryProviderId == command.LocalDeliveryProviderId &&
                        e.AddressDistrictId == command.AddressDistrictId)
                    .ExecuteDeleteAsync();
            }

            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
