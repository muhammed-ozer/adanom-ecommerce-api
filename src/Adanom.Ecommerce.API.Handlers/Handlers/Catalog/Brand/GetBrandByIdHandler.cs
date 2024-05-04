namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetBrandByIdHandler : IRequestHandler<GetBrandById, BrandResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetBrandByIdHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<BrandResponse?> Handle(GetBrandById command, CancellationToken cancellationToken)
        {
            var brands = await _mediator.Send(new GetBrands());

            return brands.Rows.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
