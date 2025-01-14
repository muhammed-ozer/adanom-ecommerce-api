using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class MigrateAnonymousShoppingCartToShoppingCartHandler : IRequestHandler<MigrateAnonymousShoppingCartToShoppingCart, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public MigrateAnonymousShoppingCartToShoppingCartHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(MigrateAnonymousShoppingCartToShoppingCart command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var userId = command.Identity.GetUserId();

            if (command.AnonymousShoppingCartId == Guid.Empty)
            {
                return true;
            }

            var anonymousShoppingCart = await _mediator.Send(new GetAnonymousShoppingCart(command.AnonymousShoppingCartId));

            if (anonymousShoppingCart == null)
            {
                return true;
            }

            var anonymousShoppingCartItems = await _mediator.Send(new GetAnonymousShoppingCartItems(new GetAnonymousShoppingCartItemsFilter()
            {
                AnonymousShoppingCartId = anonymousShoppingCart.Id
            }));

            if (!anonymousShoppingCartItems.Any())
            {
                return true;
            }

            foreach (var item in anonymousShoppingCartItems)
            {
                var createShoppingCartItemRequest = _mapper.Map<CreateShoppingCartItemRequest>(item);
                var createShoppingCartItemCommand = _mapper.Map(createShoppingCartItemRequest, new CreateShoppingCartItem(command.Identity));

                try
                {
                    await _mediator.Send(createShoppingCartItemCommand);
                }
                catch
                {
                    continue;
                }
            }

            applicationDbContext.Remove(_mapper.Map<AnonymousShoppingCart>(anonymousShoppingCart));
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
