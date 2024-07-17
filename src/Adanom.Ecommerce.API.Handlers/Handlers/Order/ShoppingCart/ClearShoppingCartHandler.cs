namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class ClearShoppingCartHandler : IRequestHandler<ClearShoppingCart, bool>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public ClearShoppingCartHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(ClearShoppingCart command, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteShoppingCart(command.Identity));

            return true;
        }

        #endregion
    }
}
