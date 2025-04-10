﻿using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeletePickUpStoreHandler : IRequestHandler<DeletePickUpStore, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeletePickUpStoreHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeletePickUpStore command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var pickUpStore = await applicationDbContext.PickUpStores
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .SingleAsync();

            pickUpStore.DeletedAtUtc = DateTime.UtcNow;
            pickUpStore.DeletedByUserId = userId;

            if (pickUpStore.IsDefault)
            {
                var randomPickUpStore = await applicationDbContext.PickUpStores
                    .Where(e => e.DeletedAtUtc == null && e.Id != command.Id)
                    .FirstOrDefaultAsync();

                if (randomPickUpStore != null)
                {
                    randomPickUpStore.IsDefault = true;
                }
            }

            await applicationDbContext.SaveChangesAsync();

            await _mediator.Publish(new ClearEntityCache<PickUpStoreResponse>());

            return true;
        }

        #endregion
    }
}
