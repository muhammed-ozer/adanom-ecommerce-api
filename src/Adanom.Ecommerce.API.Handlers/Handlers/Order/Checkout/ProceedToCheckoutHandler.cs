namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class ProceedToCheckoutHandler : IRequestHandler<ProceedToCheckout, bool>
    {
        #region Fields

        #endregion

        #region Ctor

        public ProceedToCheckoutHandler()
        {
        }

        #endregion

        #region IRequestHandler Members

        public Task<bool> Handle(ProceedToCheckout command, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        #endregion
    }
}
