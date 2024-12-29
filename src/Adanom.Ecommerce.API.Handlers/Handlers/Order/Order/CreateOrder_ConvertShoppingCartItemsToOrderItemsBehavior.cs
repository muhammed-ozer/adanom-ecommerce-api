using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrder_ConvertShoppingCartItemsToOrderItems : IPipelineBehavior<CreateOrder, OrderResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateOrder_ConvertShoppingCartItemsToOrderItems(
            ApplicationDbContext applicationDbContext,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<OrderResponse?> Handle(CreateOrder command, RequestHandlerDelegate<OrderResponse?> next, CancellationToken cancellationToken)
        {
            var orderResponse = await next();

            if (orderResponse == null)
            {
                return null;
            }

            var userId = command.Identity.GetUserId();

            var user = await _mediator.Send(new GetUser(userId));

            if (user == null)
            {
                return null;
            }

            var shoppingCart = await _mediator.Send(new GetShoppingCart(command.Identity, false));

            if (shoppingCart == null)
            {
                return null;
            }

            var shoppingCartItems = await _mediator.Send(new GetShoppingCartItems(new GetShoppingCartItemsFilter()
            {
                ShoppingCartId = shoppingCart.Id
            }));

            if (!shoppingCartItems.Any())
            {
                return null;
            }

            var orderItems = new List<OrderItem>();

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var calculatedItemResponse = await _mediator.Send(new CalculateShoppingCartItemTotalsForCheckoutAndOrder(shoppingCartItem, user));

                if (calculatedItemResponse == null)
                {
                    return null;
                }

                var orderItem = new OrderItem()
                {
                    OrderId = orderResponse.Id,
                    ProductId = shoppingCartItem.ProductId,
                    Price = shoppingCartItem.DiscountedPrice ?? shoppingCartItem.OriginalPrice,
                    Amount = shoppingCartItem.Amount,
                    AmountUnit = calculatedItemResponse.StockUnitType.Name,
                    TaxRate = calculatedItemResponse.TaxRate,
                    TaxTotal = calculatedItemResponse.TaxTotal,
                    SubTotal = shoppingCartItem.OriginalPrice * shoppingCartItem.Amount,
                    Total = (shoppingCartItem.DiscountedPrice ?? shoppingCartItem.OriginalPrice) * shoppingCartItem.Amount,
                    DiscountTotal = (calculatedItemResponse.SubTotal - calculatedItemResponse.SubDiscountedTotal) ?? 0,
                };

                orderItems.Add(orderItem);

                var productSKU = calculatedItemResponse.ProductSKU;

                productSKU.StockQuantity -= orderItem.Amount;

                _applicationDbContext.Update(_mapper.Map<ProductSKU>(productSKU));
                await _applicationDbContext.SaveChangesAsync();
            }

            var orderItemResponses = _mapper.Map<IEnumerable<OrderItemResponse>>(orderItems);
            orderResponse.Items = orderItemResponses.ToList();

            return orderResponse;
        }

        #endregion
    }
}
