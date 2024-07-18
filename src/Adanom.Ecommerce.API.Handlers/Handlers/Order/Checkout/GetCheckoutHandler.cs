namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetCheckoutHandler : IRequestHandler<GetCheckout, CheckoutResponse?>
    {
        #region Fields

        #endregion

        #region Ctor

        public GetCheckoutHandler()
        {
        }

        #endregion

        #region IRequestHandler Members

        public Task<CheckoutResponse?> Handle(GetCheckout command, CancellationToken cancellationToken)
        {
            return Task.FromResult(new CheckoutResponse() ?? null);
        }

        #endregion
    }
}
