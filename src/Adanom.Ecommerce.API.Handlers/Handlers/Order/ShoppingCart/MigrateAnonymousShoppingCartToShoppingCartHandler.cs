using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class MigrateAnonymousShoppingCartToShoppingCartHandler : IRequestHandler<MigrateAnonymousShoppingCartToShoppingCart, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public MigrateAnonymousShoppingCartToShoppingCartHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(MigrateAnonymousShoppingCartToShoppingCart command, CancellationToken cancellationToken)
        {
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
                var createShoppingCartItemResponse = await _mediator.Send(createShoppingCartItemCommand);

                if (!createShoppingCartItemResponse)
                {
                    continue;
                }
            }

            _applicationDbContext.Remove(_mapper.Map<AnonymousShoppingCart>(anonymousShoppingCart));

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.SHOPPINGCART,
                    TransactionType = TransactionType.CREATE,
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
