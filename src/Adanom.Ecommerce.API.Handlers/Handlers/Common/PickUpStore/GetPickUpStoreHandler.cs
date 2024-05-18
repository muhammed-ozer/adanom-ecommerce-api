namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetPickUpStoreHandler : IRequestHandler<GetPickUpStore, PickUpStoreResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetPickUpStoreHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PickUpStoreResponse?> Handle(GetPickUpStore command, CancellationToken cancellationToken)
        {
            var pickUpStores = await _mediator.Send(new GetPickUpStores());
           
            return pickUpStores.Rows.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
