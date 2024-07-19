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
                response.HasNoItem = true;

                return response;
            }

            foreach (var item in shoppingCartItems)
            {
                var productPrice = await _mediator.Send(new GetProductPriceByProductId(item.ProductId));
                var productSKU = await _mediator.Send(new GetProductSKUByProductId(item.ProductId));

                if ((productPrice == null || productSKU == null) && command.Identity != null)
                {
                    await DeleteShoppingCartItemAsync(command.Identity, item.Id);

                    response.HasProductDeleted = true;

                    continue;
                }

                if (productPrice!.OriginalPrice != item.OriginalPrice)
                {
                    item.OriginalPrice = productPrice.OriginalPrice;

                    response.HasPriceChanges = true;
                }

                if (productPrice!.DiscountedPrice != item.DiscountedPrice)
                {
                    item.DiscountedPrice = productPrice.DiscountedPrice;

                    response.HasPriceChanges = true;
                }

                if (productSKU!.StockQuantity == 0 && command.Identity != null)
                {
                    await DeleteShoppingCartItemAsync(command.Identity, item.Id);

                    response.HasProductDeleted = true;

                    continue;
                }

                if (item.Amount > productSKU.StockQuantity)
                {
                    item.Amount = productSKU.StockQuantity;

                    response.HasStocksChanges = true;

                    continue;
                }
            }

            if (response.HasProductDeleted || response.HasPriceChanges || response.HasStocksChanges)
            {
                var shoppingCartId = shoppingCartItems.Select(e => e.ShoppingCartId).FirstOrDefault();

                await _mediator.Send(new UpdateShoppingCart_LastModifiedDate(shoppingCartId));
                await _applicationDbContext.SaveChangesAsync();

                var shoppingCartItemsCount = await _mediator.Send(new GetShoppingCartItemsCount(command.Identity!));

                if (shoppingCartItemsCount == 0) 
                {
                    response.HasNoItem = true;
                }
            }

            return response;
        }

        #endregion

        #region Private Methods

        private async Task DeleteShoppingCartItemAsync(ClaimsPrincipal identity, long shoppingCartItemId)
        {
            var deleteShoppingCartItemRequest = new DeleteShoppingCartItemRequest()
            {
                Id = shoppingCartItemId
            };

            var deleteShoppingCartItemCommand = _mapper.Map(deleteShoppingCartItemRequest, new DeleteShoppingCartItem(identity));

            await _mediator.Send(deleteShoppingCartItemCommand);
        }

        #endregion
    }
}
