﻿using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateLocalDeliveryProvider_AddressDistrictHandler : IRequestHandler<CreateLocalDeliveryProvider_AddressDistrict, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateLocalDeliveryProvider_AddressDistrictHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateLocalDeliveryProvider_AddressDistrict command, CancellationToken cancellationToken)
        {
            var localDeliveryProvider_AddressDistrict = new LocalDeliveryProvider_AddressDistrict_Mapping()
            {
                LocalDeliveryProviderId = command.LocalDeliveryProviderId,
                AddressDistrictId = command.AddressDistrictId
            };

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(localDeliveryProvider_AddressDistrict);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
