using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrder_ConvertShoppingCartItemsToOrderItems : IPipelineBehavior<CreateOrder, OrderResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateOrder_ConvertShoppingCartItemsToOrderItems(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
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

            var shoppingCart = await _mediator.Send(new GetShoppingCart(command.Identity, true, false, false, command.OrderPaymentType));

            if (shoppingCart == null)
            {
                return null;
            }

            if (!shoppingCart.Items.Any())
            {
                return null;
            }

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var orderItems = new List<OrderItem>();

            foreach (var shoppingCartItem in shoppingCart.Items)
            {
                var shoppingCartItemSummary = await _mediator.Send(new CalculateShoppingCartItemSummary(shoppingCartItem, user, command.OrderPaymentType));

                if (shoppingCartItemSummary == null)
                {
                    return null;
                }

                var productSKU = await _mediator.Send(new GetProductSKUByProductId(shoppingCartItem.ProductId));

                if (productSKU == null)
                {
                    return null;
                }

                var stockUnitType = await _mediator.Send(new GetStockUnitType(productSKU.StockUnitType.Key));

                var orderItem = new OrderItem()
                {
                    OrderId = orderResponse.Id,
                    ProductId = shoppingCartItem.ProductId,
                    Price = shoppingCartItem.DiscountedPrice ?? shoppingCartItem.OriginalPrice,
                    Amount = shoppingCartItem.Amount,
                    AmountUnit = stockUnitType.Name,
                    TaxRate = shoppingCartItemSummary.TaxRate,
                    TaxTotal = shoppingCartItemSummary.TaxTotal,
                    SubTotal = shoppingCartItemSummary.SubTotal,
                    Total = shoppingCartItemSummary.SubTotal - shoppingCartItemSummary.DiscountTotal ?? 0,
                    DiscountTotal = shoppingCartItemSummary.DiscountTotal ?? 0,
                };

                if (shoppingCartItemSummary.UserDefaultDiscountRateBasedDiscount != null && shoppingCartItemSummary.UserDefaultDiscountRateBasedDiscount > 0)
                {
                    if (orderResponse.UserDefaultDiscountRateBasedDiscount == null)
                    {
                        orderResponse.UserDefaultDiscountRateBasedDiscount = 0;
                    }

                    orderResponse.UserDefaultDiscountRateBasedDiscount += shoppingCartItemSummary.UserDefaultDiscountRateBasedDiscount;
                }

                if (shoppingCartItemSummary.DiscountByOrderPaymentType != null && shoppingCartItemSummary.DiscountByOrderPaymentType > 0)
                {
                    if (orderResponse.DiscountByOrderPaymentType == null)
                    {
                        orderResponse.DiscountByOrderPaymentType = 0;
                    }

                    orderResponse.DiscountByOrderPaymentType += shoppingCartItemSummary.DiscountByOrderPaymentType;
                }

                orderItems.Add(orderItem);
            }

            var orderItemResponses = _mapper.Map<IEnumerable<OrderItemResponse>>(orderItems);
            orderResponse.Items = orderItemResponses.ToList();

            // Delete shopping cart if payment type is not online payment
            if (command.OrderPaymentType != OrderPaymentType.ONLINE_PAYMENT)
            {
                await _mediator.Send(new DeleteShoppingCart(command.Identity));
            }


            return orderResponse;
        }

        #endregion
    }
}
