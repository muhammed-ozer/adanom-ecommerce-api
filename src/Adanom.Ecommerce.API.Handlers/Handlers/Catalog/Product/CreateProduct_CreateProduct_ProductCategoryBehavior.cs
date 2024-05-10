namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_CreateProduct_ProductCategoryBehavior : IPipelineBehavior<CreateProduct, ProductResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProduct_CreateProduct_ProductCategoryBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<ProductResponse?> Handle(CreateProduct command, RequestHandlerDelegate<ProductResponse?> next, CancellationToken cancellationToken)
        {
            var productResponse = await next();

            if (productResponse == null)
            {
                return null;
            }

            var createProduct_ProductCategoryCommand = new CreateProduct_ProductCategory(command.Identity)
            {
                ProductCategoryId = command.ProductCategoryId,
                ProductId = productResponse.Id
            };

            var createProduct_ProductCategoryResponse = await _mediator.Send(createProduct_ProductCategoryCommand);

            if (!createProduct_ProductCategoryResponse)
            {
                return null;
            }

            return productResponse;
        }

        #endregion
    }
}
