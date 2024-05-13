namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductCategoryHandler : IRequestHandler<GetProductCategory, ProductCategoryResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetProductCategoryHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductCategoryResponse?> Handle(GetProductCategory command, CancellationToken cancellationToken)
        {
            var productCategories = await _mediator.Send(new GetProductCategories());

            if (command.UrlSlug.IsNotNullOrEmpty())
            {
                return productCategories.Rows.SingleOrDefault(e => e.UrlSlug == command.UrlSlug);
            }

            return productCategories.Rows.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
