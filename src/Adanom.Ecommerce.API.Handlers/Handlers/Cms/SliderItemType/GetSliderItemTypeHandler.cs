namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetSliderItemTypeHandler : IRequestHandler<GetSliderItemType, SliderItemTypeResponse?>

    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetSliderItemTypeHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<SliderItemTypeResponse?> Handle(GetSliderItemType command, CancellationToken cancellationToken)
        {
            var stockUnitTypes = await _mediator.Send(new GetSliderItemTypes());

            return stockUnitTypes.SingleOrDefault(e => e.Key == command.SliderItemType);
        }

        #endregion
    }
}
