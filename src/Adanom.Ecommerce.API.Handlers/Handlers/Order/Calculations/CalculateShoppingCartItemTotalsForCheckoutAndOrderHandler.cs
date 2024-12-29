namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CalculateShoppingCartItemTotalsForCheckoutAndOrderHandler
        : IRequestHandler<CalculateShoppingCartItemTotalsForCheckoutAndOrder, CalculateShoppingCartItemTotalsForCheckoutAndOrderResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CalculateShoppingCartItemTotalsForCheckoutAndOrderHandler(
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

            var subTotal = shoppingCartItem.OriginalPrice * shoppingCartItem.Amount;
            decimal? subDiscountedTotal = null;

            if (shoppingCartItem.DiscountedPrice != null && shoppingCartItem.DiscountedPrice.Value != 0)
            {
                subDiscountedTotal = shoppingCartItem.DiscountedPrice.Value * shoppingCartItem.Amount;
            }
            else if (user.DefaultDiscountRate > 0)
            {
                var discountAmount = Calculations.CalculateDiscountedPriceByDiscountRate(shoppingCartItem.OriginalPrice, user.DefaultDiscountRate);
                shoppingCartItem.DiscountedPrice = shoppingCartItem.OriginalPrice - discountAmount;
                subDiscountedTotal = shoppingCartItem.DiscountedPrice * shoppingCartItem.Amount;
            }

            decimal taxTotal;

            if (subDiscountedTotal != null && subDiscountedTotal.Value > 0)
            {
                taxTotal = Calculations.CalculateTaxFromIncludedTaxTotal(subDiscountedTotal.Value, taxCategory.Rate / 100m);
            }
            else
            {
                taxTotal = Calculations.CalculateTaxFromIncludedTaxTotal(subTotal, taxCategory.Rate / 100m);
            }

            return new CalculateShoppingCartItemTotalsForCheckoutAndOrderResponse()
            {
                StockUnitType = stockUnitType,
                ProductSKU = productSKU,
                TaxRate = taxCategory.Rate,
                TaxTotal = taxTotal,
                SubTotal = subTotal,
                SubDiscountedTotal = subDiscountedTotal
            };
        }

        #endregion
    }
}
