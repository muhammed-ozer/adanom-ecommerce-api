﻿using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteTaxCategoryHandler : IRequestHandler<DeleteTaxCategory, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteTaxCategoryHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteTaxCategory command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var taxCategory = await applicationDbContext.TaxCategories
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            taxCategory.DeletedAtUtc = DateTime.UtcNow;
            taxCategory.DeletedByUserId = userId;

            await applicationDbContext.SaveChangesAsync();

            await _mediator.Publish(new RemoveFromCache<TaxCategoryResponse>(taxCategory.Id));

            return true;
        }

        #endregion
    }
}
