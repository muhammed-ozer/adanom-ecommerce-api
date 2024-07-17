using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateShoppingCartItemsHandler : IRequestHandler<UpdateShoppingCartItems, UpdateShoppingCartItemsResponse>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateShoppingCartItemsHandler(
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

        public async Task<UpdateShoppingCartItemsResponse> Handle(UpdateShoppingCartItems command, CancellationToken cancellationToken)
        {
            var shoppingCartItemsQuery = _applicationDbContext.ShoppingCartItems.AsQueryable();

            var shoppingCartItems = new List<ShoppingCartItem>();

            if (command.UserId != null && command.UserId != Guid.Empty)
            {
                shoppingCartItems = await shoppingCartItemsQuery
                    .Where(e => e.ShoppingCart.UserId == command.UserId)
                    .ToListAsync();
            }
            else if (command.Identity != null)
            {
                var userId = command.Identity.GetUserId();

                shoppingCartItems = await shoppingCartItemsQuery
                   .Where(e => e.ShoppingCart.UserId == userId)
                   .ToListAsync();
            }
            else
            {
                shoppingCartItems = await shoppingCartItemsQuery
                   .Where(e => e.ShoppingCart.Id == command.ShoppingCartId)
                   .ToListAsync();
            }

            var response = new UpdateShoppingCartItemsResponse();

            if (!shoppingCartItems.Any())
            {
                return response;
            }

            foreach (var item in shoppingCartItems)
            {
                var productPrice = await _mediator.Send(new GetProductPriceByProductId(item.ProductId));

                if (productPrice == null && command.Identity != null)
                {
                    var deleteShoppingCartItemRequest = new DeleteShoppingCartItemRequest()
                    {
                        Id = item.Id
                    };

                    var deleteShoppingCartItemCommand = _mapper.Map(deleteShoppingCartItemRequest, new DeleteShoppingCartItem(command.Identity));

                    await _mediator.Send(deleteShoppingCartItemCommand);

                    response.HasProductDeleted = true;

                    continue;
                }

                var price = productPrice!.DiscountedPrice ?? productPrice.OriginalPrice;

                if (price != item.Price)
                {
                    item.Price = price;

                    response.HasPriceChanges = true;
                }
            }

            await _applicationDbContext.SaveChangesAsync();

            return response;
        }

        #endregion
    }
}
