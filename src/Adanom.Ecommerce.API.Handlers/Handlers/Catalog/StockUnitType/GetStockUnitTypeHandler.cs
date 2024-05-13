namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetStockUnitTypeHandler : IRequestHandler<GetStockUnitType, StockUnitTypeResponse?>

    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetStockUnitTypeHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<StockUnitTypeResponse?> Handle(GetStockUnitType command, CancellationToken cancellationToken)
        {
            var stockUnitTypes = await _mediator.Send(new GetStockUnitTypes());

            return stockUnitTypes.SingleOrDefault(e => e.Key == command.StockUnitType);
        }

        #endregion
    }
}
