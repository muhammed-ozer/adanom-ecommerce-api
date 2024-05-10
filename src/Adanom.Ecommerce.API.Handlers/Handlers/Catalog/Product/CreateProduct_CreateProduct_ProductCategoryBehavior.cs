namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_CreateProduct_ProductCategoryBehavior : IPipelineBehavior<CreateProduct, ProductResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateProduct_CreateProduct_ProductCategoryBehavior(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

            var createProduct_ProductCategoryRequest = new CreateProduct_ProductCategoryRequest()
            {
                ProductId = productResponse.Id,
                ProductCategoryId = command.ProductCategoryId
            };

            var createProduct_ProductCategoryCommand = _mapper.Map(createProduct_ProductCategoryRequest, new CreateProduct_ProductCategory(command.Identity));

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
