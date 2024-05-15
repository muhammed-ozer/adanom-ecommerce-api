namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetAddressDistrictHandler : IRequestHandler<GetAddressDistrict, AddressDistrictResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetAddressDistrictHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<AddressDistrictResponse?> Handle(GetAddressDistrict command, CancellationToken cancellationToken)
        {
            var addressDistricts = await _mediator.Send(new GetAddressDistricts());

            return addressDistricts.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
