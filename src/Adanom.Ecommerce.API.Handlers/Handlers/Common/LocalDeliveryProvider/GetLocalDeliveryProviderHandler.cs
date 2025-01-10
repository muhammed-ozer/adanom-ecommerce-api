namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetLocalDeliveryProviderHandler : IRequestHandler<GetLocalDeliveryProvider, LocalDeliveryProviderResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetLocalDeliveryProviderHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<LocalDeliveryProviderResponse?> Handle(GetLocalDeliveryProvider command, CancellationToken cancellationToken)
        {
            var localDeliveryProviders = await _mediator.Send(new GetLocalDeliveryProviders());
           
            return localDeliveryProviders.Rows.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
