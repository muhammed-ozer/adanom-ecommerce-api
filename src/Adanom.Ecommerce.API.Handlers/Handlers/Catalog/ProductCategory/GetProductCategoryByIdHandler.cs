namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductCategoryByIdHandler : IRequestHandler<GetProductCategoryById, ProductCategoryResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetProductCategoryByIdHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductCategoryResponse?> Handle(GetProductCategoryById command, CancellationToken cancellationToken)
        {
            var productCategories = await _mediator.Send(new GetProductCategories());

            return productCategories.Rows.SingleOrDefault(e => e.Id == command.Id);
        } 

        #endregion
    }
}
