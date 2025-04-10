﻿namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteAnonymousShoppingCartHandler : IRequestHandler<DeleteAnonymousShoppingCart, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteAnonymousShoppingCartHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteAnonymousShoppingCart command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var anonymousShoppingCart = await applicationDbContext.AnonymousShoppingCarts
                .Where(e => e.Id == command.Id)
                .SingleOrDefaultAsync();

            if (anonymousShoppingCart == null)
            {
                return true;
            }

            applicationDbContext.Remove(anonymousShoppingCart);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
