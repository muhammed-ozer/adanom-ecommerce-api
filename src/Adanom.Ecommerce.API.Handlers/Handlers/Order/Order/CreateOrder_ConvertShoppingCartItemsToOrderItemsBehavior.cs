using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrder_ConvertShoppingCartItemsToOrderItems : IPipelineBehavior<CreateOrder, OrderResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ICalculationService _calculationService;

        #endregion

        #region Ctor

        public CreateOrder_ConvertShoppingCartItemsToOrderItems(
            ApplicationDbContext applicationDbContext,
            IMediator mediator,
            IMapper mapper,
            ICalculationService calculationService)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
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
                    TaxExcludedPrice = _calculationService.CalculateTaxExcludedPrice(shoppingCartItem.DiscountedPrice ?? shoppingCartItem.OriginalPrice, calculatedItemResponse.TaxRate),
                    Amount = shoppingCartItem.Amount,
                    AmountUnit = calculatedItemResponse.StockUnitType.Name,
                    TaxRate = calculatedItemResponse.TaxRate,
                    TaxTotal = calculatedItemResponse.TaxTotal,
                    SubTotal = calculatedItemResponse.SubTotal,
                    Total = (calculatedItemResponse.SubDiscountedTotal ?? calculatedItemResponse.SubTotal) + calculatedItemResponse.TaxTotal,
                    DiscountTotal = (calculatedItemResponse.SubTotal - calculatedItemResponse.SubDiscountedTotal) ?? 0,
                };

                if (calculatedItemResponse.UserDefaultDiscountRateBasedDiscount != null && calculatedItemResponse.UserDefaultDiscountRateBasedDiscount > 0)
                {
                    if (orderResponse.UserDefaultDiscountRateBasedDiscount == null)
                    {
                        orderResponse.UserDefaultDiscountRateBasedDiscount = calculatedItemResponse.UserDefaultDiscountRateBasedDiscount;
                    }
                    else
                    {
                        orderResponse.UserDefaultDiscountRateBasedDiscount += calculatedItemResponse.UserDefaultDiscountRateBasedDiscount;
                    }
                }

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
