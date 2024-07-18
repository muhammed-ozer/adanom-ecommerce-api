namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrder_CalculateTotalBehavior : IPipelineBehavior<CreateOrder, OrderResponse?>
    {
        #region Fields

        #endregion

        #region Ctor

        public CreateOrder_CalculateTotalBehavior()
        {
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

            orderResponse.GrandTotal = orderResponse.Items.Sum(e => e.Total) + orderResponse.ShippingFeeSubTotal + orderResponse.ShippingFeeTax;
            orderResponse.SubTotal = orderResponse.Items.Sum(e => e.SubTotal) + orderResponse.ShippingFeeSubTotal;
            orderResponse.SubTotalDiscount = orderResponse.Items.Sum(e => e.DiscountTotal);
            orderResponse.TaxTotal = orderResponse.Items.Sum(e => e.TaxTotal) + orderResponse.ShippingFeeTax;

            return orderResponse;
        }

        #endregion
    }
}
