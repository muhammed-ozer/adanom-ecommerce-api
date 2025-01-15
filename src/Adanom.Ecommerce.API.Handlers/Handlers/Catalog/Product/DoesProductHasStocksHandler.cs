namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductHasStocksHandler : IRequestHandler<DoesProductHasStocks, bool>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DoesProductHasStocksHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProductHasStocks command, CancellationToken cancellationToken)
        {
            var stocks = await _mediator.Send(new GetProductStockQuantity(command.ProductId));

            if (command.Amount != null)
            {
                return stocks >= command.Amount.Value;
            }

            return stocks != 0;
        }

        #endregion
    }
}
