namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductCategoryByUrlSlugHandler : IRequestHandler<GetProductCategoryByUrlSlug, ProductCategoryResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetProductCategoryByUrlSlugHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductCategoryResponse?> Handle(GetProductCategoryByUrlSlug command, CancellationToken cancellationToken)
        {
            var productCategories = await _mediator.Send(new GetProductCategories());

            return productCategories.Rows.SingleOrDefault(e => e.UrlSlug == command.UrlSlug);
        } 

        #endregion
    }
}
