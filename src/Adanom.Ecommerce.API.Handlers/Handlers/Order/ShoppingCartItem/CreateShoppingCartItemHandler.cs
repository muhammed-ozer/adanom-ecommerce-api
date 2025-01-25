using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateShoppingCartItemHandler : IRequestHandler<CreateShoppingCartItem, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateShoppingCartItemHandler(
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

        public async Task<bool> Handle(CreateShoppingCartItem command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var userId = command.Identity.GetUserId();

            var shoppingCart = await _mediator.Send(new GetShoppingCart(userId, false));

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
                        target.OriginalPrice = productPrice.OriginalPrice;
                        target.DiscountedPrice = productPrice.DiscountedPrice;
                    });
                });

                await applicationDbContext.AddAsync(shoppingCartItem);
            }
            else
            {
                var shoppingCartItem = _mapper.Map<ShoppingCartItem>(shoppingCartItemResponse);

                shoppingCartItem.Amount += command.Amount;
                shoppingCartItem.LastModifiedAtUtc = DateTime.UtcNow;
                shoppingCartItem.OriginalPrice = productPrice.OriginalPrice;
                shoppingCartItem.DiscountedPrice = productPrice.DiscountedPrice;

                applicationDbContext.Update(shoppingCartItem);
            }

            await _mediator.Send(new UpdateShoppingCart_LastModifiedDate(shoppingCart.Id));

            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
