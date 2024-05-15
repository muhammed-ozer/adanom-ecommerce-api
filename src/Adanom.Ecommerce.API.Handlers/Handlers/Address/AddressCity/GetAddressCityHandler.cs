namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAddressCityHandler : IRequestHandler<GetAddressCity, AddressCityResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetAddressCityHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<AddressCityResponse?> Handle(GetAddressCity command, CancellationToken cancellationToken)
        {
            var addressCities = await _mediator.Send(new GetAddressCities());

            return addressCities.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
