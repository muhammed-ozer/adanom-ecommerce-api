namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetBrandHandler : IRequestHandler<GetBrand, BrandResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetBrandHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<BrandResponse?> Handle(GetBrand command, CancellationToken cancellationToken)
        {
            var brands = await _mediator.Send(new GetBrands());

            if (command.UrlSlug.IsNotNullOrEmpty())
            {
                return brands.Rows.SingleOrDefault(e => e.UrlSlug == command.UrlSlug);
            }

            return brands.Rows.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
