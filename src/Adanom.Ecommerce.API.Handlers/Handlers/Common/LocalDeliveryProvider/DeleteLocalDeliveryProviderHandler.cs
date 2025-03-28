﻿using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteLocalDeliveryProviderHandler : IRequestHandler<DeleteLocalDeliveryProvider, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteLocalDeliveryProviderHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteLocalDeliveryProvider command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var localDeliveryProvider = await applicationDbContext.LocalDeliveryProviders
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .SingleAsync();

            localDeliveryProvider.DeletedAtUtc = DateTime.UtcNow;
            localDeliveryProvider.DeletedByUserId = userId;

            if (localDeliveryProvider.IsDefault)
            {
                var randomLocalDeliveryProvider = await applicationDbContext.LocalDeliveryProviders
                    .Where(e => e.DeletedAtUtc == null && e.Id != command.Id)
                    .FirstOrDefaultAsync();

                if (randomLocalDeliveryProvider != null)
                {
                    randomLocalDeliveryProvider.IsDefault = true;
                }
            }

            await applicationDbContext.SaveChangesAsync();
           
            await _mediator.Publish(new ClearEntityCache<LocalDeliveryProviderResponse>());

            return true;
        }

        #endregion
    }
}
