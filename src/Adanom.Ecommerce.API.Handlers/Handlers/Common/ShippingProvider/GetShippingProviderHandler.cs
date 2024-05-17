namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShippingProviderHandler : IRequestHandler<GetShippingProvider, ShippingProviderResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetShippingProviderHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ShippingProviderResponse?> Handle(GetShippingProvider command, CancellationToken cancellationToken)
        {
            var shippingProviders = await _mediator.Send(new GetShippingProviders());
           
            return shippingProviders.Rows.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
