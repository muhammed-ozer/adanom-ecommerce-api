namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetBrandByUrlSlugHandler : IRequestHandler<GetBrandByUrlSlug, BrandResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetBrandByUrlSlugHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<BrandResponse?> Handle(GetBrandByUrlSlug command, CancellationToken cancellationToken)
        {
            var brands = await _mediator.Send(new GetBrands());

            return brands.Rows.SingleOrDefault(e => e.UrlSlug == command.UrlSlug);
        } 

        #endregion
    }
}
