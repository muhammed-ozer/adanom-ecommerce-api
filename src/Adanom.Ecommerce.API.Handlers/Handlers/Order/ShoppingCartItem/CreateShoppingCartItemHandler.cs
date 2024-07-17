using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateShoppingCartItemHandler : IRequestHandler<CreateShoppingCartItem, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateShoppingCartItemHandler(
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

        public async Task<bool> Handle(CreateShoppingCartItem command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var shoppingCart = await _mediator.Send(new GetShoppingCart(userId));

            if (shoppingCart == null)
            {
                shoppingCart = await _mediator.Send(new CreateShoppingCart(command.Identity));

                if (shoppingCart == null)
                {
                    return false;
                }
            }

            var shoppingCartItemResponse = await _mediator.Send(new GetShoppingCartItem(userId, command.ProductId));
            var productPrice = await _mediator.Send(new GetProductPriceByProductId(command.ProductId));

            if (productPrice == null)
            {
                return false;
            }

            if (shoppingCartItemResponse == null)
            {
                var shoppingCartItem = _mapper.Map<CreateShoppingCartItem, ShoppingCartItem>(command, options =>
                {
                    options.AfterMap((source, target) =>
                    {
                        target.ShoppingCartId = shoppingCart.Id;
                        target.LastModifiedAtUtc = DateTime.UtcNow;
                        target.Price = productPrice.DiscountedPrice ?? productPrice.OriginalPrice;
                    });
                });

                await _applicationDbContext.AddAsync(shoppingCartItem);
            }
            else
            {
                var shoppingCartItem = _mapper.Map<ShoppingCartItem>(shoppingCartItemResponse);

                shoppingCartItem.Amount += command.Amount;
                shoppingCartItem.LastModifiedAtUtc = DateTime.UtcNow;
                shoppingCartItem.Price = productPrice.DiscountedPrice ?? productPrice.OriginalPrice; ;

                _applicationDbContext.Update(shoppingCartItem);
            }

            await _mediator.Send(new UpdateShoppingCart_LastModifiedDate(shoppingCart.Id));

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.SHOPPINGCARTITEM,
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
