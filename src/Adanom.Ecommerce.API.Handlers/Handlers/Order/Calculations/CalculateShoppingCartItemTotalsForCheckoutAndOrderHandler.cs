namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CalculateShoppingCartItemTotalsForCheckoutAndOrderHandler
        : IRequestHandler<CalculateShoppingCartItemTotalsForCheckoutAndOrder, CalculateShoppingCartItemTotalsForCheckoutAndOrderResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ICalculationService _calculationService;

        #endregion

        #region Ctor

        public CalculateShoppingCartItemTotalsForCheckoutAndOrderHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator,
            ICalculationService calculationService)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<CalculateShoppingCartItemTotalsForCheckoutAndOrderResponse?> Handle(CalculateShoppingCartItemTotalsForCheckoutAndOrder command, CancellationToken cancellationToken)
        {
            var shoppingCartItem = command.ShoppingCartItem;
            var user = command.User;

            var productPrice = await _mediator.Send(new GetProductPriceByProductId(shoppingCartItem.ProductId));

            if (productPrice == null)
            {
                return null;
            }

            var taxCategory = await _mediator.Send(new GetTaxCategory(productPrice.TaxCategoryId));

            if (taxCategory == null)
            {
                return null;
            }

            var productSKU = await _mediator.Send(new GetProductSKUByProductId(shoppingCartItem.ProductId));

            if (productSKU == null)
            {
                return null;
            }

            var response = new CalculateShoppingCartItemTotalsForCheckoutAndOrderResponse();

            var stockUnitType = await _mediator.Send(new GetStockUnitType(productSKU.StockUnitType.Key));

            var taxExludedOriginalPrice = _calculationService.CalculateTaxExcludedPrice(shoppingCartItem.OriginalPrice, taxCategory.Rate);
            var subTotal = taxExludedOriginalPrice * shoppingCartItem.Amount;

            decimal? subDiscountedTotal = null;
            decimal? userDefaultDiscountRateBasedDiscountTotal = null;

            if (shoppingCartItem.DiscountedPrice != null && shoppingCartItem.DiscountedPrice.Value != 0)
            {
                var taxExludedDiscountedPrice = _calculationService.CalculateTaxExcludedPrice(shoppingCartItem.DiscountedPrice.Value, taxCategory.Rate);

                subDiscountedTotal = taxExludedDiscountedPrice * shoppingCartItem.Amount;
            }
            else if (user.DefaultDiscountRate > 0)
            {
                shoppingCartItem.DiscountedPrice = _calculationService.CalculateDiscountedPriceByDiscountRate(shoppingCartItem.OriginalPrice, user.DefaultDiscountRate);
                var taxExludedDiscountedPrice = _calculationService.CalculateTaxExcludedPrice(shoppingCartItem.DiscountedPrice.Value, taxCategory.Rate);

                subDiscountedTotal = taxExludedDiscountedPrice * shoppingCartItem.Amount;

                userDefaultDiscountRateBasedDiscountTotal = shoppingCartItem.Amount * (taxExludedOriginalPrice - taxExludedDiscountedPrice);
            }

            decimal taxTotal;

            if (subDiscountedTotal != null && subDiscountedTotal.Value > 0)
            {
                var tax = _calculationService.CalculateTaxTotal(shoppingCartItem.DiscountedPrice!.Value, taxCategory.Rate);

                taxTotal = shoppingCartItem.Amount * tax;
            }
            else
            {
                var tax = _calculationService.CalculateTaxTotal(shoppingCartItem.OriginalPrice, taxCategory.Rate);

                taxTotal = shoppingCartItem.Amount * tax;
            }

            return new CalculateShoppingCartItemTotalsForCheckoutAndOrderResponse()
            {
                StockUnitType = stockUnitType,
                ProductSKU = productSKU,
                TaxRate = taxCategory.Rate,
                TaxTotal = taxTotal,
                UserDefaultDiscountRateBasedDiscount = userDefaultDiscountRateBasedDiscountTotal,
                SubTotal = subTotal,
                SubDiscountedTotal = subDiscountedTotal
            };
        }

        #endregion
    }
}
