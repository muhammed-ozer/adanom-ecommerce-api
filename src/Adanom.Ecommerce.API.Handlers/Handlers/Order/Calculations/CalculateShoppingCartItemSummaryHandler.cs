﻿namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CalculateShoppingCartItemSummaryHandler
        : IRequestHandler<CalculateShoppingCartItemSummary, CalculateShoppingCartItemSummaryResponse?>
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ICalculationService _calculationService;

        #endregion

        #region Ctor

        public CalculateShoppingCartItemSummaryHandler(
            IMapper mapper,
            IMediator mediator,
            ICalculationService calculationService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<CalculateShoppingCartItemSummaryResponse?> Handle(CalculateShoppingCartItemSummary command, CancellationToken cancellationToken)
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

            var subTotal = shoppingCartItem.OriginalPrice * shoppingCartItem.Amount;

            decimal? subDiscountedTotal = null;
            decimal? userDefaultDiscountRateBasedDiscountTotal = null;
            decimal? discountByOrderPaymentType = null;

            if (shoppingCartItem.DiscountedPrice != null && shoppingCartItem.DiscountedPrice.Value != 0)
            {
                subDiscountedTotal = shoppingCartItem.DiscountedPrice.Value * shoppingCartItem.Amount;
            }
            else if (user.DefaultDiscountRate > 0)
            {
                shoppingCartItem.DiscountedPrice = _calculationService.CalculateDiscountedPriceByDiscountRate(shoppingCartItem.OriginalPrice, user.DefaultDiscountRate);

                subDiscountedTotal = shoppingCartItem.DiscountedPrice.Value * shoppingCartItem.Amount;

                userDefaultDiscountRateBasedDiscountTotal = subTotal - subDiscountedTotal;
            }

            if (command.OrderPaymentType != null)
            {
                var orderPaymentType = await _mediator.Send(new GetOrderPaymentType(command.OrderPaymentType.Value));

                if (orderPaymentType != null && orderPaymentType.DiscountRate > 0)
                {
                    discountByOrderPaymentType = _calculationService.CalculateDiscountByDiscountRate(subDiscountedTotal ?? subTotal, orderPaymentType.DiscountRate);

                    shoppingCartItem.DiscountedPrice = _calculationService.CalculateDiscountedPriceByDiscountRate(
                        shoppingCartItem.DiscountedPrice ?? shoppingCartItem.OriginalPrice,
                        orderPaymentType.DiscountRate);

                    subDiscountedTotal = shoppingCartItem.DiscountedPrice.Value * shoppingCartItem.Amount;
                }
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

            return new CalculateShoppingCartItemSummaryResponse()
            {
                TaxRate = taxCategory.Rate,
                TaxTotal = taxTotal,
                UserDefaultDiscountRateBasedDiscount = userDefaultDiscountRateBasedDiscountTotal,
                DiscountByOrderPaymentType = discountByOrderPaymentType,
                SubTotal = subTotal,
                DiscountTotal = (subTotal - subDiscountedTotal) ?? 0
            };
        }

        #endregion
    }
}
